using AutoMapper.Configuration;
using leave_management.Contracts;
using leave_management.Services.Components.ORI;
using leave_management.Services.DataSeeds;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data.Seeds
{
    public class SeedUsersAndOrganizations : IDataSeed
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IOrganizationResourceManager _organizationManager;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAuthorizedOrganizationRepository _authorizedOrganizationRepository;

        public SeedUsersAndOrganizations(
            UserManager<Employee> userManager,
            IOrganizationResourceManager organizationManager,
            IOrganizationRepository organizationRepository,
            IAuthorizedOrganizationRepository authorizedOrganizationRepository)
        {
            _userManager = userManager;
            _organizationManager = organizationManager;
            _organizationRepository = organizationRepository;
            _authorizedOrganizationRepository = authorizedOrganizationRepository;

        }
        public void Seed()
        {
            if (_userManager.FindByNameAsync("admin@stercore.pl").Result == null)
            {

                var organizationToken = _organizationManager.GenerateToken();

                var initalAuthorizedOrganization = new AuthorizedOrganizations
                {
                    AuthorizedOrganizationToken = organizationToken
                };

                var successAuthOrg = _authorizedOrganizationRepository.Create(initalAuthorizedOrganization).Result;

                var initalOrganization = new Organization
                {
                    Name = "Westapp",
                    Code = "WST1",
                    TaxId = "5751900764",
                    Street = "Wiśniowa",
                    HouseNumber = "11",
                    City = "Lubliniec",
                };


                var successOrg = _organizationRepository.Create(initalOrganization, organizationToken).Result;


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
                        var result = _userManager.CreateAsync(user, Environment.GetEnvironmentVariable("ADMINISTRATOR_PASSWORD")).Result;
                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }

                    else
                    {
                        var result = _userManager.CreateAsync(user, "P@ssword1").Result;
                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }


                }
            }
        }
    }
}