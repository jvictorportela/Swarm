using Microsoft.AspNetCore.Mvc;
using Swarm.Api.Attributes;
using Swarm.Application.UseCases.Group.Create;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Api.Controllers;

[AuthenticatedUser]
public class GroupController : SwarmBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredGroupJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterGroupUseCase useCase, [FromBody] RequestGroupJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
