using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using nursat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nursat.Models
{
    public class AdminCreate
    {
        private AppDb db;
        private UserManager<User> um;
        private RoleManager<IdentityRole> rm;



        public AdminCreate(AppDb _db, UserManager<User> _um, RoleManager<IdentityRole> _roleManager)
        {
            db = _db;
            um = _um;
            rm = _roleManager;
        }

        

        public async Task<bool> CreateAsync()
        {
            User user = new User { UserName = "nursat", Email = "nursat" };

            var result = await um.CreateAsync(user, "nursat2021");

            await rm.CreateAsync(new IdentityRole("admin"));

            var role = await rm.Roles.FirstAsync();

            var val = await um.AddToRoleAsync(user, role.Name);

            await db.SaveChangesAsync();

            return true;
        }


    }
}
