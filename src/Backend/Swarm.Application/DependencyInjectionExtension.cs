using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using Swarm.Application.Services.AutoMapper;
using Swarm.Application.UseCases.Group.Create;
using Swarm.Application.UseCases.Login.DoLogin;
using Swarm.Application.UseCases.User.ChangePassword;
using Swarm.Application.UseCases.User.Profile;
using Swarm.Application.UseCases.User.Register;
using Swarm.Application.UseCases.User.Update;

namespace Swarm.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services, configuration);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IRegisterGroupUseCase, RegisterGroupUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
        });

        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping(sqids));
        }).CreateMapper());
    }
}
