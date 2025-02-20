using System.Net;
using Agenda.Application.Validators;
using Agenda.Domain.Responses;
using FluentValidation;
using Agenda.Domain.Models.Agenda;
using Agenda.Domain.Repositories.Agenda;

namespace Agenda.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserModel> _userValidator;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userValidator = new UserValidation();
    }

    public async Task<Result<UserModel>> RegisterAsync(UserModel userModel)
    {
        var validationResult = await _userValidator.ValidateAsync(userModel);
        if (!validationResult.IsValid)
        {
            return Result<UserModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _userRepository.AddAsync(userModel);
        return Result<UserModel>.Success(
            userModel,
            HttpStatusCode.Created,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.Created)
        );
    }

    public async Task<Result<UserModel>> GetByIdAsync(Guid id)
    {
        var userModel = await _userRepository.GetByIdAsync(id);
        if (userModel is null)
        {
            return Result<UserModel>.Failure(
                HttpStatusMessages.GetMessage((int)HttpStatusCode.NotFound),
                HttpStatusCode.NotFound
            );
        }

        return Result<UserModel>.Success(
            userModel,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<List<UserModel>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return Result<List<UserModel>>.Success(
            users.ToList(),
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<UserModel>> UpdateAsync(UserModel userModel)
    {
        var validationResult = await _userValidator.ValidateAsync(userModel);
        if (!validationResult.IsValid)
        {
            return Result<UserModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                HttpStatusCode.BadRequest
            );
        }

        await _userRepository.UpdateAsync(userModel);
        return Result<UserModel>.Success(
            userModel,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        return Result<bool>.Success(
            true,
            HttpStatusCode.OK,
            HttpStatusMessages.GetMessage((int)HttpStatusCode.OK)
        );
    }
}
