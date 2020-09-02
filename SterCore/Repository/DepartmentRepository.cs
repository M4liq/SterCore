using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _organizationManager.SetAccess(entity);
            await _db.Department.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Create(Department entity, bool areTokensProvided)
        {
            if(!areTokensProvided)
            {
                _organizationManager.SetAccess(entity);
                await _db.Department.AddAsync(entity);
                return await Save();
            }

            await _db.Department.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Department entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Department.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Department>> FindAll()
        {
            var departments = _organizationManager.FilterDbSetByView(_db.Department);
            return await departments.ToListAsync();       
        }

        public async Task<Department> FindById(int id)
        {
            var departments = _organizationManager.FilterDbSetByView(_db.Department);
            return await departments.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Department entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Department.Update(entity);
            return await Save();
        }
    }
}
