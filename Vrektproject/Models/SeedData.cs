using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Vrektproject.Data;

namespace Vrektproject.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            

            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }

                context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = "0",
                        Name = "Admin"
                    },
                    new IdentityRole
                    {
                        Id = "1",
                        Name = "Member"
                    },
                    new IdentityRole
                    {
                        Id = "2",
                        Name = "Recruiter"
                    });
                context.SaveChanges();
            }
        }
    }
}