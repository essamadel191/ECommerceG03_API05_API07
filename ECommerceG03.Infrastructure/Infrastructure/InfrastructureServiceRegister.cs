using ECommerceG03.Domain.Contracts;
using ECommerceG03.Infrastructure.Data;
using ECommerceG03.Infrastructure.DataSeeding;
using ECommerceG03.Infrastructure.Repository;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerceG03.Infrastructure.Identity.Data;
using ECommerceG03.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceG03.Infrastructure.Infrastructure
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your infrastructure services here
            // For example, if you have a repository or a database context, you can register them like this:
            //services.AddScoped<IYourRepository, YourRepositoryImplementation>();
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<StoreIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeed>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<ICacheRepository,CacheRepository>();


            // Register Redis connection multiplexer as a singleton so it can be injected where needed
            // Use configuration value "Redis:ConnectionString" if present, otherwise fall back to localhost:6379
            var redisConnectionString = configuration.GetSection("Redis:ConnectionString").Value ?? "localhost:6379";
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectionString));
            //services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
