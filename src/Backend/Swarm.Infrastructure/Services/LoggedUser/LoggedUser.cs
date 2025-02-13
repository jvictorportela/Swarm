using Microsoft.EntityFrameworkCore;
using Swarm.Domain.Entities;
using Swarm.Domain.Security.Tokens;
using Swarm.Domain.Services.LoggedUser;
using Swarm.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Swarm.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly SwarmDbContext _context;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(SwarmDbContext context, ITokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}
