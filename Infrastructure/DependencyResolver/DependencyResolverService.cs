using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterInfrastrucureLayer(IServiceCollection service)
    {
        service.AddScoped<IBikeShopRepository, BikeShopRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
    }
}