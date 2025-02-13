using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
