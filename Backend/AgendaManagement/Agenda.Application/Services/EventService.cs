using System.Net;
using Agenda.Domain.Dtos;
using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;
using Agenda.Domain.Responses;
using Agenda.Application.Validators.Event;
using FluentValidation;
using Warehouse.Domain.Responses;

namespace Agenda.Application.Services;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IValidator<EventModel> _eventValidator;
    private readonly IValidator<DateTime> _dateValidator;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
        _eventValidator = new EventValidation();
        _dateValidator = new EventFilterValidation();
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

        var createdEvent = await _eventRepository.CreateAsync(eventModel);
        return Result<EventModel>.Success(
            createdEvent, 
            HttpStatusCode.Created, 
            HttpStatusMessages.GetMessage((int)HttpStatusCode.Created)
        );
    }

    public async Task<Result<EventModel>> GetByIdAsync(int id)
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
            events, 
            HttpStatusCode.OK, 
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<List<EventModel>>> GetByDateAsync(DateTime date)
    {
        var validationResult = await _dateValidator.ValidateAsync(date);
        if (!validationResult.IsValid)
        {
            return Result<List<EventModel>>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        var events = await _eventRepository.GetByDateAsync(date);
        return Result<List<EventModel>>.Success(
            events, 
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

        var updatedEvent = await _eventRepository.UpdateAsync(eventModel);
        return Result<EventModel>.Success(
            updatedEvent, 
            HttpStatusCode.OK, 
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var deleted = await _eventRepository.DeleteAsync(id);
        if (!deleted)
        {
            return Result<bool>.Failure(
                HttpStatusMessages.GetMessage((int)HttpStatusCode.NotFound), 
                HttpStatusCode.NotFound
            );
        }

        return Result<bool>.Success(
            true, 
            HttpStatusCode.OK, 
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }
}
