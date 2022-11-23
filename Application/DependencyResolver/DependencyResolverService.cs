using API.Services;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;

public class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection service)
    {
        service.AddScoped<IBikeShopService, BikeShopService>();
        service.AddScoped<IAuthenticationService, AuthenticationService>();
    }
    
}