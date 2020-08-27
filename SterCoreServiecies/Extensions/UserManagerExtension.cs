using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace leave_management.Services.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<Organization> ExtGetUserOrganization(this UserManager<Employee> userManager, ClaimsPrincipal userPrincipal, IEmployeeRepository employeeRepository)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            var employee = await employeeRepository.FindById(user.Id);
            return employee.Organization;
        }

    }
}
