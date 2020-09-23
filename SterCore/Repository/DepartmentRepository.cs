using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace leave_management.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Department> _organizationManager;

        public DepartmentRepository(ApplicationDbContext db, IOrganizationResourceManager<Department> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }

        public async Task<bool> Create(Department entity)
        {
            //ORI separating data between organizations
            var token = _organizationManager.GetDepartmentToken();
            entity.OrganizationToken = _organizationManager.GenerateToken();
            entity.DepartmentToken = _organizationManager.GenerateToken();

            var authorizedOrganizationId = await _organizationManager.GetAuthorizedDepartmentId((token));

            //If our organization is not set as authorized organization our organization must be set as superior organization 
            //for created organization otherwise getting access to created organization would be impossible 
            if (authorizedOrganizationId == -1)
            {
                var authorize = new AuthorizedDepartment()
                {
                    AuthorizedDepartmentToken = token
                };

                entity.AuthorizedDepartment = authorize;
            }
            else
                entity.AuthorizedDepartmentId = authorizedOrganizationId;

            await _db.Department.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Create(Department entity, string authorizedToken)
        {
            entity.AuthorizedDepartmentId = await _organizationManager.GetAuthorizedDepartmentId(authorizedToken);

                await _db.Department.AddAsync(entity);
                return await Save();
        }

        public async Task<bool> Delete(Department entity)
        {
            var token = _organizationManager.GetDepartmentToken();
            var validate = await _db.Department
                .Where(q => q.AuthorizedDepartment.AuthorizedDepartmentToken == token || q.DepartmentToken == token)
                .AnyAsync(q => q.Id == entity.Id);

            if (validate)
                _db.Department.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await FindById(id) == null;
        }

        public async Task<ICollection<Department>> FindAll()
        {
            //ORI getting token to find organization scope
            var departmentToken = _organizationManager.GetDepartmentToken();

            //ORI Filtring leave types by their tokens to get scope
            var organizations = _db.Department
                .Where(q => q.AuthorizedDepartment.AuthorizedDepartmentToken == departmentToken ||
                            q.DepartmentToken == departmentToken || 
                            q.DepartmentToken == q.AuthorizedDepartment.AuthorizedDepartmentToken)
                .Include(q => q.Organization);

            return await organizations.ToListAsync();

        }

        public async Task<Department> FindById(int id)
        {
            //ORI getting token to find organization scope
            var departmentToken = _organizationManager.GetDepartmentToken();

            return await _db.Department
                .Where(q => q.AuthorizedDepartment.AuthorizedDepartmentToken == departmentToken 
                            || q.DepartmentToken == departmentToken
                            || q.DepartmentToken == q.AuthorizedDepartment.AuthorizedDepartmentToken)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Department> FindInitialDepartment(Organization organization)
        {
            return await _db.Department
                .Where(q => q.OrganizationId == organization.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Department entity)
        {
            var token = _organizationManager.GetDepartmentToken();
            var validate = await _db.Department
                .Where(q => q.AuthorizedDepartment.AuthorizedDepartmentToken == token || q.DepartmentToken == token)
                .AnyAsync(q => q.Id == entity.Id);

            if (validate)
                _db.Department.Update(entity);
            return await Save();
        }

    }
}
