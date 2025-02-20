using System.Net;
using Agenda.Application.Validators;
using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Domain.Responses;
using FluentValidation;

namespace Agenda.Application.Services;

public class AgendaService
{
    private readonly IAgendaRepository _agendaRepository;
    private readonly IValidator<AgendaModel> _agendaValidator;

    public AgendaService(IAgendaRepository agendaRepository)
    {
        _agendaRepository = agendaRepository;
        _agendaValidator = new AgendaValidation();
    }

    public async Task<Result<AgendaModel>> CreateAsync(AgendaModel agendaModel)
    {
        var validationResult = await _agendaValidator.ValidateAsync(agendaModel);
        if (!validationResult.IsValid)
        {
            return Result<AgendaModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _agendaRepository.AddAsync(agendaModel);
        return Result<AgendaModel>.Success(
            agendaModel,
            HttpStatusCode.Created,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.Created)
        );
    }

    public async Task<Result<AgendaModel>> GetByIdAsync(Guid id)
    {
        var agenda = await _agendaRepository.GetByIdAsync(id);
        if (agenda is null)
        {
            return Result<AgendaModel>.Failure(
                HttpStatusMessages.GetMessage((int)HttpStatusCode.NotFound),
                HttpStatusCode.NotFound
            );
        }

        return Result<AgendaModel>.Success(
            agenda,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<List<AgendaModel>>> GetAllAsync()
    {
        var agendas = await _agendaRepository.GetAllAsync();
        return Result<List<AgendaModel>>.Success(
            agendas.ToList(),
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<AgendaModel>> UpdateAsync(AgendaModel agendaModel)
    {
        var validationResult = await _agendaValidator.ValidateAsync(agendaModel);
        if (!validationResult.IsValid)
        {
            return Result<AgendaModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _agendaRepository.UpdateAsync(agendaModel);
        return Result<AgendaModel>.Success(
            agendaModel,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        await _agendaRepository.DeleteAsync(id);
        return Result<bool>.Success(
            true,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }
}