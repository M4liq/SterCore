using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
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
            IOrganizationResourceManager organizationManager,
            IAuthorizedOrganizationRepository authorizedOrganizationRepository,
            ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUp,
            IConfiguration configuration)
        {
            //Architecture based on events would be better
            SeedRoles(roleManager); 
            SeedUsersAndOrganizations(userManager, organizationManager, organizationRepository, authorizedOrganizationRepository, configuration); //setting up both Users and Organizations is very messy
            SeedMedicalCheckUpTypes(typeOfMedicalCheckUp);
        }

        private static void  SeedUsersAndOrganizations(
            UserManager<Employee> userManager,
            IOrganizationResourceManager organizationManager, 
            IOrganizationRepository organizationRepository, 
            IAuthorizedOrganizationRepository authorizedOrganizationRepository,
            IConfiguration configuration
            )
        { 

            if(userManager.FindByNameAsync("admin@stercore.pl").Result == null) 
            {

                var organizationToken = organizationManager.GenerateToken();

                var initalAuthorizedOrganization = new AuthorizedOrganizations
                {
                    AuthorizedOrganizationToken = organizationToken
                };

                var successAuthOrg = authorizedOrganizationRepository.Create(initalAuthorizedOrganization).Result;

                var initalOrganization = new Organization 
                {
                    Name = "Westapp",
                    Code = "WST1",
                    TaxId = "5751900764",
                    Street = "Wiśniowa",
                    HouseNumber = "11",
                    City = "Lubliniec",   
                };


                var successOrg = organizationRepository.Create(initalOrganization, organizationToken).Result;


                if (successOrg)
                {
                    var user = new Employee
                    {
                        UserName = "admin@stercore.pl",
                        Email = "admin@stercore.pl",
                        OrganizationId = initalOrganization.Id,
                        OrganizationToken = organizationToken, //adding organization token, cause it is not handled by user manager
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

            if (!roleManager.RoleExistsAsync("Agent").Result)
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

        private static void SeedMedicalCheckUpTypes(ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUp)
        {
            if (typeOfMedicalCheckUp.FindAll().Result.Count==0)
            {
                List<TypeOfMedicalCheckUp> entity = new List<TypeOfMedicalCheckUp>();
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie wstępne", 200));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie okresowe", 201));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie kontrolne", 202));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie końcowe", 203));
                entity.Add(new TypeOfMedicalCheckUp("Badanie psychotechniczne", 204));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne terminowe", 205));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne bezterminowe", 206));
                foreach (var item in entity)
                {
                    var result = typeOfMedicalCheckUp.Create(item).Result;
                }
            }
            
            
        }
    }
}
