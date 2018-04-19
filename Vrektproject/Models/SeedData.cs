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
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var profile = new Profile();
                context.Add(profile);
                context.SaveChanges();

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

                context.Users.Add(
                     new ApplicationUser
                     {
                         Id = "0",
                         UserName = "Admin",
                         Email = "admin@vrekt.com",
                         PasswordHash = "AQAAAAEAACcQAAAAELIWKUx9T3thpdzviWIODaUZ3cas/nAfW1JVFTY2CY9INUa4bFrolc6e/oqfJ6vD9w==",
                         ProfileId = profile.Id,
                         RoleIdentifier = 0,
                         Authorized = true
                     });
                context.SaveChanges();

                context.UserRoles.Add(
                    new IdentityUserRole<string>
                    {
                        UserId = "0",
                        RoleId = "0"
                    });
                context.SaveChanges();
            }
        }
    }
}