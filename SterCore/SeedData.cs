using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedData //Class requires reconstruction 
    {
        public static void Seed(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            IOrganizationRepository organizationRepository,
            IConfiguration configuration)
        {
            //Architecture based on events would be better
            SeedRoles(roleManager); 
            SeedUsersAndOrganizations(userManager, organizationRepository, configuration); //setting up both Users and Organizations is very messy
          
        }

        private static void  SeedUsersAndOrganizations(
            UserManager<Employee> userManager, 
            IOrganizationRepository organizationRepository, 
            IConfiguration configuration
            )
        { 

            if(userManager.FindByNameAsync("admin@stercore.pl").Result == null) 
            {
                var initalOrganization = new Organization 
                {
                    Name = "Westapp",
                    Code = "WST1",
                    TaxId = "5751900764",
                    Street = "Wiśniowa",
                    HouseNumber = "11",
                    City = "Lubliniec"

                };

                var success = organizationRepository.Create(initalOrganization).Result;

                if (success)
                {
                    var user = new Employee
                    {
                        UserName = "admin@stercore.pl",
                        Email = "admin@stercore.pl",
                        OrganizationId = initalOrganization.Id,
                        ChangedPassword = true
                    };

                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                    {
                       var result = userManager.CreateAsync(user, configuration["AdministratorPSWD"]).Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }
                
                    else
                    {
                        var result = userManager.CreateAsync(user, "P@ssword1").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }    


                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("AccountingManager").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Agent"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employer").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employer"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
