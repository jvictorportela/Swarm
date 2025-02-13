using Swarm.Communication.Requests;
using Swarm.Domain.Repositories.User;
using Swarm.Domain.Repositories;
using Swarm.Domain.Services.LoggedUser;
using Swarm.Exceptions.ExceptionBase;
using Swarm.Exceptions;
using Swarm.Domain.Extensions;

namespace Swarm.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository repository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.User();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();
        var result = await validator.ValidateAsync(request);

        if (currentEmail.Equals(request.Email).IsFalse())
        {
            var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesExceptions.EMAIL_ALLREADY_REGISTERED));
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
