using ECommerceG03.Domain.Contracts;
using ECommerceG03.Infrastructure.Identity.Data;
using ECommerceG03.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.DataSeeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext context
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , ILogger<IdentityDataSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigratios = _context.Database.GetPendingMigrations();
                if (pendingMigratios.Any()) await _context.Database.MigrateAsync(ct);

                // Seed roles
                if (!await _roleManager.Roles.AnyAsync(ct))
                {
                    var roles = new List<IdentityRole>
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("SuperAdmin")
                };
                    foreach (var role in roles)
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }

                // Seed users
                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        DisplayName = "Admin",
                        EmailConfirmed = true
                    };
                    var superAdminUser = new ApplicationUser
                    {
                        UserName = "superadmin",
                        Email = "superAdmin@gmail.com",
                        DisplayName = "Super Admin",
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(adminUser, "Admin@123");
                    _logger.LogInformation($"Admin user creation result: {result.Succeeded}");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                        _logger.LogInformation("Admin user created and assigned to Admin role.");
                    }

                    var superAdminResult = await _userManager.CreateAsync(superAdminUser, "SuperAdmin@123");
                    _logger.LogInformation($"SuperAdmin user creation result: {superAdminResult.Succeeded}");
                    if (superAdminResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                        _logger.LogInformation("SuperAdmin user created and assigned to SuperAdmin role.");
                    }
                    else
                    {
                        var errors = string.Join(", ", superAdminResult.Errors.Select(e => e.Description));
                        _logger.LogError($"Failed to create super admin user: {errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
    }
}
