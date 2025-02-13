using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swarm.Application.Services.AutoMapper;
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
        AddAutoMapper(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
}
