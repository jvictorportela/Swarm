using AutoMapper;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;
using Swarm.Domain.Repositories.User;
using Swarm.Domain.Repositories;
using Swarm.Domain.Security.Criptography;
using Swarm.Domain.Security.Tokens;
using Swarm.Exceptions.ExceptionBase;
using Swarm.Exceptions;

namespace Swarm.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IMapper _mapper;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository,
        IMapper mapper,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _mapper = mapper;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        //Validar a request
        await Validate(request);

        //Mapear a request em uma entidade
        var user = _mapper.Map<Domain.Entities.User>(request);

        //Criptografar a senha
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        //Salvar no banco de dados
        await _writeRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExist = await _readRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_ALLREADY_REGISTERED));

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
