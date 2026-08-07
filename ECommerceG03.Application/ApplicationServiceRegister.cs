using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceG03.Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegister).Assembly);

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            

            return services;
        }
    }
}
