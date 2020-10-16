using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Helpers.Enums;

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
                if (!_roleManager.RoleExistsAsync(RoleEnum.Administrator).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = RoleEnum.Administrator
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }

                if (!_roleManager.RoleExistsAsync(RoleEnum.Agent).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = RoleEnum.Agent
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }

                if (!_roleManager.RoleExistsAsync(RoleEnum.Employee).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = RoleEnum.Employee
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }
                if (!_roleManager.RoleExistsAsync(RoleEnum.Employer).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = RoleEnum.Employer
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }

                if (!_roleManager.RoleExistsAsync(RoleEnum.Manager).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = RoleEnum.Manager
                    };
                    var result = _roleManager.CreateAsync(role).Result;
                }
        }
    }
}
