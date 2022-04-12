using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain;

namespace TodoList.Identity.Service.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitTestUser(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var testUser = new ApplicationUser { UserName = "test" };
                await userManager.CreateAsync(testUser, "pwd123");

                var diyaUser = new ApplicationUser { UserName = "diya" };
                await userManager.CreateAsync(diyaUser, "pwd123");
            }

        }
    }
}
