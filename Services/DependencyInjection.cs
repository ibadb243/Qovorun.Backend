using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IMembersService, MemberService>();
        services.AddScoped<IShortnameService, ShortnameService>();
        
        return services;
    }
}