using Swarm.Communication.Requests;
using Swarm.Communication.Responses;
using Swarm.Domain.Repositories.User;
using Swarm.Domain.Security.Criptography;
using Swarm.Domain.Security.Tokens;
using Swarm.Exceptions.ExceptionBase;

namespace Swarm.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository repository,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encrypterPassword = _passwordEncrypter.Encrypt(request.Password);

        var user = await _repository.GetByEmailAndPassword(request.Email, encrypterPassword) ?? throw new InvalidLoginException();
        //?? se devolver nulo, executa o que está após.

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
