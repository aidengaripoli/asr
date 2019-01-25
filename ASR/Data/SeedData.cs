using ASR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Data
{
    public static class SeedData
    {
        public static async Task InitialiseAsync(IServiceProvider serviceProvider)
        {
            using (var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>())
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            using (var context = new ASRContext(serviceProvider.GetRequiredService<DbContextOptions<ASRContext>>()))
            {
                await InitialiseRolesAsync(roleManager);
                await InitialiseRoomsAsync(context);
                await context.SaveChangesAsync();
            }
        }

        public static async Task InitialiseRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in new[] { Constants.StaffRole, Constants.StudentRole })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }

        private static async Task InitialiseRoomsAsync(ASRContext context)
        {
            if (await context.Room.AnyAsync())
                return; // return if rooms already exist

            await context.Room.AddRangeAsync(
                new Room { RoomID = "A" },
                new Room { RoomID = "B" },
                new Room { RoomID = "C" },
                new Room { RoomID = "D" }
            );
        }
    }
}
