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
                new IdentityUser { UserName = "Benjamin Graham", Email = "graham@jobsity.com" },
                new IdentityUser { UserName = "John Templeton", Email = "templeton@jobsity.com" },
                new IdentityUser { UserName = "John Neff", Email = "neff@jobsity.com" },
                new IdentityUser { UserName = "Jesse Livermore", Email = "livermore@jobsity.com" },
                new IdentityUser { UserName = "Peter Lynch", Email = "lynch@jobsity.com" },
                new IdentityUser { UserName = "George Soros", Email = "soros@jobsity.com" },
                new IdentityUser { UserName = "Warren Buffett", Email = "buffett@jobsity.com" },
                new IdentityUser { UserName = "John Bogle", Email = "bogle@jobsity.com" },
                new IdentityUser { UserName = "Carl Icahn", Email = "icahn@jobsity.com" },
                new IdentityUser { UserName = "William H. Gross", Email = "gross@jobsity.com" },
            };

            foreach (var defaultUser in defaultUsers)
            {
                if (userManager.Users.All(u => u.UserName.Equals(defaultUser.UserName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await userManager.CreateAsync(defaultUser, "1234");
                }
            }            
        }
    }
}
