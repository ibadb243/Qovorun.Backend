using Application.Interfaces;
using Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Contexts;

namespace Presistence;

public static class DependencyInjection
{
    /* TODO: implement contexts interfaces */
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DefaultConnection"];
        
        services.AddDbContext<UserDbContext>(options => options.UseSqlite(connectionString));
        services.AddDbContext<GroupDbContext>(options => options.UseSqlite(connectionString));
        services.AddDbContext<MemberDbContext>(options => options.UseSqlite(connectionString));
        services.AddDbContext<MessageDbContext>(options => options.UseSqlite(connectionString));
        services.AddDbContext<ChannelDbContext>(options => options.UseSqlite(connectionString));
        services.AddDbContext<ShortnameDbContext>(options => options.UseSqlite(connectionString));
        
        services.AddScoped<IUserDbContext>(provider => provider.GetService<UserDbContext>()!);
        services.AddScoped<IGroupDbContext>(provider => provider.GetService<GroupDbContext>()!);
        services.AddScoped<IMemberDbContext>(provider => provider.GetService<MemberDbContext>()!);
        services.AddScoped<IMessageDbContext>(provider => provider.GetService<MessageDbContext>()!);
        services.AddScoped<IChannelDbContext>(provider => provider.GetService<ChannelDbContext>()!);
        services.AddScoped<IShortnameDbContext>(provider => provider.GetService<ShortnameDbContext>()!);
        
        return services;
    }
}