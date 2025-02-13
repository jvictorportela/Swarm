using Microsoft.AspNetCore.Mvc;
using Swarm.Api.Filters;

namespace Swarm.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
