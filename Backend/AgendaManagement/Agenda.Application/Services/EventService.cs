using System.Net;
using Agenda.Application.Validators.Event;
using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Domain.Responses;
using FluentValidation;

namespace Agenda.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IValidator<EventModel> _eventValidator;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
        _eventValidator = new EventValidation();
    }

    public async Task<Result<EventModel>> CreateAsync(EventModel eventModel)
    {
        var validationResult = await _eventValidator.ValidateAsync(eventModel);
        if (!validationResult.IsValid)
        {
            return Result<EventModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _eventRepository.AddAsync(eventModel);
        return Result<EventModel>.Success(
            eventModel,
            HttpStatusCode.Created,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.Created)
        );
    }

    public async Task<Result<EventModel>> GetByIdAsync(Guid id)
    {
        var eventModel = await _eventRepository.GetByIdAsync(id);
        if (eventModel is null)
        {
            return Result<EventModel>.Failure(
                HttpStatusMessages.GetMessage((int)HttpStatusCode.NotFound),
                HttpStatusCode.NotFound
            );
        }

        return Result<EventModel>.Success(
            eventModel,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<List<EventModel>>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return Result<List<EventModel>>.Success(
            events.ToList(),
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<EventModel>> UpdateAsync(EventModel eventModel)
    {
        var validationResult = await _eventValidator.ValidateAsync(eventModel);
        if (!validationResult.IsValid)
        {
            return Result<EventModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _eventRepository.UpdateAsync(eventModel);
        return Result<EventModel>.Success(
            eventModel,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        await _eventRepository.DeleteAsync(id);
        return Result<bool>.Success(
            true,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }
}