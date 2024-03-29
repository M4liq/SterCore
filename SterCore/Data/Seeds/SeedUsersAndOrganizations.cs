﻿using AutoMapper.Configuration;
using leave_management.Contracts;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
using leave_management.Services.DataSeeds;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using leave_management.Helpers.Enums;
using leave_management.Services.LeaveHelper.Contracts;

namespace leave_management.Data.Seeds
{
    public class SeedUsersAndOrganizations : IDataSeed
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IOrganizationResourceManager _organizationManager;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAuthorizedOrganizationRepository _authorizedOrganizationRepository;
        private readonly IAuthorizedDepartmentRepository _authorizedDepartmentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public SeedUsersAndOrganizations(
            UserManager<Employee> userManager,
            IOrganizationResourceManager organizationManager,
            IOrganizationRepository organizationRepository,
            IAuthorizedOrganizationRepository authorizedOrganizationRepository,
            IAuthorizedDepartmentRepository authorizedDepartmentRepository,
            IDepartmentRepository departmentRepository)
        {
            _userManager = userManager;
            _organizationManager = organizationManager;
            _organizationRepository = organizationRepository;
            _authorizedOrganizationRepository = authorizedOrganizationRepository;
            _authorizedDepartmentRepository = authorizedDepartmentRepository;
            _departmentRepository = departmentRepository;
        }

        public void Seed()
        {
            if (_userManager.FindByNameAsync("admin@stercore.pl").Result != null)
                return;
            
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
                    Disabled = false,
                    InialOrganization = true
                };

                    var successOrg = _organizationRepository.Create(initalOrganization, organizationToken).Result;

                var departmentToken = _organizationManager.GenerateToken();

                    var initialAuthorizedDepartment = new AuthorizedDepartment()
                    {
                        AuthorizedDepartmentToken = departmentToken
                    };

                    var successAuthDep = _authorizedDepartmentRepository.Create(initialAuthorizedDepartment).Result;

                //There is potential of using Strategy Pattern
                if (successOrg)
                {
                    var department = new Department
                    {
                        Name = "Administracja",
                        Code = "ADM",
                        DateCreated = DateTime.Now,
                        OrganizationToken = organizationToken,
                        InitialDepartment = true,
                        OrganizationId = initalOrganization.Id,
                        DepartmentToken = departmentToken
                    };

                    var successDep = _departmentRepository.Create(department, departmentToken).Result;

                    var user = new Employee
                    {
                        UserName = "admin@stercore.pl",
                        Email = "admin@stercore.pl",
                        DepartmentId = department.Id,
                        ChangedPassword = true,
                        InitialAdministrator = true,
                        DepartmentToken = departmentToken,
                        OrganizationToken = organizationToken
                    };

                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                    {
                        var result = _userManager.CreateAsync(user, Environment.GetEnvironmentVariable("ADMINISTRATOR_PASSWORD")).Result;
                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(user, RoleEnum.Administrator).Wait();
                        }
                    }

                    else
                    {
                        var result = _userManager.CreateAsync(user, "P@ssword1").Result;
                        if (result.Succeeded)
                        {
                            _userManager.AddToRoleAsync(user, RoleEnum.Administrator).Wait();
                        }
                    }
                }
        }
    }
}