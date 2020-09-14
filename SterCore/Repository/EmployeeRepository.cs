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
        private readonly IOrganizationResourceManager<Employee> _organizationManager;

        public EmployeeRepository(ApplicationDbContext db, IOrganizationResourceManager<Employee> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(Employee entity)
        {
            _organizationManager.SetAccess(entity);

            await _db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            _organizationManager.VerifyAccess(entity);
            _db.Employees.Remove(entity); 
            return await Save();
        }

        public async Task<bool> Exists(string id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Employee>> FindAll()
        {
            var leaveTypes = _organizationManager.FilterDbSetByView(_db.Employees).Include(q => q.Department);

            return await leaveTypes.ToListAsync();
        }

        public async Task<Employee> FindById(string id)
        {
            var employee = await _organizationManager.FilterDbSetByView(_db.Employees)
                .Include(q => q.Department)
                .ThenInclude(q => q.Organization)
                .FirstOrDefaultAsync(q => q.Id == id);

            return employee;
        }

        public async Task<Employee> FindById(string id, bool disableORI)
        {
            if(disableORI)
            {
                var employee = await _db.Employees
                    .Include(q => q.Department)
                    .ThenInclude(q => q.Organization)
                    .FirstOrDefaultAsync(q => q.Id == id);
                return employee;
            }
            else
            {
                var employee = await _organizationManager.FilterDbSetByView(_db.Employees)
                    .Include(q => q.Department)
                    .ThenInclude(q => q.Organization)
                    .FirstOrDefaultAsync(q => q.Id == id);
                return employee;
            }

        }

        public async Task<ICollection<Employee>> FindUsersWithEmail(string email)
        {
            //without filtering
            var employees = _db.Employees.Where(q => q.Email == email).ToListAsync();
            return await employees;
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

            var roles = await _db.Roles.Where(q => q.Name != "Agent" && q.Name != "Administrator").ToListAsync();
            return roles;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Employee entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Employees.Update(entity);
            return await Save();
        }
    }
}
