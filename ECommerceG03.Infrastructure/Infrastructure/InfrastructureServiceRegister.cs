using ECommerceG03.Domain.Contracts;
using ECommerceG03.Infrastructure.Data;
using ECommerceG03.Infrastructure.DataSeeding;
using ECommerceG03.Infrastructure.Repository;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using ECommerceG03.Infrastructure.Identity.Data;
using ECommerceG03.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

            // Get Data From appsettings.json
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>()
                ?? throw new InvalidOperationException("JWT settings are not configured");
            
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeed>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<ICacheRepository,CacheRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenSerivce>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // In case Token Successded
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // In case Token Failed "Unauthorized"
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    // Validations
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    RequireExpirationTime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),

                    ClockSkew = TimeSpan.FromMinutes(5), // Extra Time
                };

            });


            // Register Redis connection multiplexer as a singleton so it can be injected where needed
            // Use configuration value "Redis:ConnectionString" if present, otherwise fall back to localhost:6379
            var redisConnectionString = configuration.GetSection("Redis:ConnectionString").Value ?? "localhost:6379";
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectionString));
            //services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
