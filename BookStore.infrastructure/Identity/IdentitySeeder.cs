using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.infrastructure.Identity;
public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<long>> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "User"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<long>
                {
                    Name = role
                });
            }
        }
    }
}
