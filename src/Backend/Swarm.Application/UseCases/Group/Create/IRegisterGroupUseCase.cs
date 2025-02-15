using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Application.UseCases.Group.Create;

public interface IRegisterGroupUseCase
{
    Task<ResponseRegisteredGroupJson> Execute(RequestGroupJson group);
}
