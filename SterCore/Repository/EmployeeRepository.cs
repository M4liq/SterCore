using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public EmployeeRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(Employee entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            await _db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.Employees.Remove(entity); 
            return await Save();
        }

        public async Task<bool> Exists(string id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.Employees

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<Employee>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var employees = await _db.Employees

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)

                .ToListAsync();
            return employees;
        }

        public async Task<Employee> FindById(string id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var employee = await _db.Employees.Include(q => q.Organization)

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)

                .FirstOrDefaultAsync(q => q.Id == id);
            return employee;
        }

        public async Task<IEnumerable<IdentityRole>> GetAdministratorIdentityRoles()
        {   
            //ORI not included 

            var roles = await _db.Roles.ToListAsync();
            return roles;
        }

        public async Task<IEnumerable<IdentityRole>> GetAgentIdentityRoles()
        {
            //ORI not included 

            var roles = await _db.Roles.ToListAsync();
            return roles.Where(q => q.Name != "Administrator");
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Employee entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.Employees.Update(entity);
            return await Save();
        }
    }
}
