using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.DataSeeds
{
    public class SeedRoles : IDataSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedRoles (RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public void Seed()
        {
                if (!_roleManager.RoleExistsAsync("Administrator").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Administrator"
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }

                if (!_roleManager.RoleExistsAsync("Agent").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Agent"
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }

                if (!_roleManager.RoleExistsAsync("Employee").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Employee"
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }
                if (!_roleManager.RoleExistsAsync("Employer").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Employer"
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }
            
        }
    }
}
