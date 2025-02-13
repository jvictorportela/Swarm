using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
