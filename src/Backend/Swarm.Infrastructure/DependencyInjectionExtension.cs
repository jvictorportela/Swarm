using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swarm.Domain.Repositories.User;
using Swarm.Domain.Repositories;
using Swarm.Domain.Security.Criptography;
using Swarm.Domain.Security.Tokens;
using Swarm.Domain.Services.LoggedUser;
using Swarm.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using System.Reflection;
using Swarm.Infrastructure.Security.Criptography;
using Swarm.Infrastructure.Services.LoggedUser;
using Swarm.Infrastructure.Security.Tokens.Access.Validator;
using Swarm.Infrastructure.Security.Tokens.Access.Generator;
using Swarm.Infrastructure.Extensions;
using Swarm.Infrastructure.DataAccess.Repositories;

namespace Swarm.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
        AddTokens(services, configuration);
        AddLoggedUser(services);
        AddPasswordEncrypter(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<SwarmDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("Swarm.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey"); //Com o Binder, consigo passar a tipagem no valor do json vindo do appsettings

        services.AddScoped<IPasswordEncrypter>(option => new Shar512Encrypter(additionalKey!));
    }
}
