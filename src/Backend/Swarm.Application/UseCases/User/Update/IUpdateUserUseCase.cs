using Swarm.Communication.Requests;

namespace Swarm.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}
