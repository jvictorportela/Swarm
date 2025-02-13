using Microsoft.AspNetCore.Mvc;
using Swarm.Application.UseCases.Login.DoLogin;
using Swarm.Communication.Requests;
using Swarm.Communication.Responses;

namespace Swarm.Api.Controllers;

public class LoginController : SwarmBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
