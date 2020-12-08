using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            var defaultUsers = new[] {
                new IdentityUser { UserName = "benjamin.graham@jobsity.com", Email = "benjamin.graham@jobsity.com" },
                new IdentityUser { UserName = "john.templeton@jobsity.com", Email = "john.templeton@jobsity.com" },
                new IdentityUser { UserName = "john.neff@jobsity.com", Email = "john.neff@jobsity.com" },
                new IdentityUser { UserName = "jesse.livermore@jobsity.com", Email = "jesse.livermore@jobsity.com" },
                new IdentityUser { UserName = "peter.lynch@jobsity.com", Email = "peter.lynch@jobsity.com" },
                new IdentityUser { UserName = "george.soros@jobsity.com", Email = "george.soros@jobsity.com" },
                new IdentityUser { UserName = "warren.buffett@jobsity.com", Email = "warren.buffett@jobsity.com" },
                new IdentityUser { UserName = "john.bogle@jobsity.com", Email = "john.bogle@jobsity.com" },
                new IdentityUser { UserName = "carl.icahn@jobsity.com", Email = "carl.icahn@jobsity.com" },
                new IdentityUser { UserName = "william.gross@jobsity.com", Email = "william.gross@jobsity.com" },
            };

            foreach (var defaultUser in defaultUsers)
            {
                if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
                {
                    await userManager.CreateAsync(defaultUser, "Jobsity@123");
                }
            }
        }
    }
}
