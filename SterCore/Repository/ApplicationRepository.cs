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
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Application> _organizationManager;
        public ApplicationRepository(ApplicationDbContext db, IOrganizationResourceManager<Application> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Application entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.Applications.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Application entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Applications.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Application>> FindAll()
        {
            var Application = _organizationManager.FilterDbSetByView(_db.Applications);
            return await Application.Include(q => q.Employee).ToListAsync();
        }

        public async Task<Application> FindById(int id)
        {
            var Application = _organizationManager.FilterDbSetByView(_db.Applications);
            return await Application.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Application entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Applications.Update(entity);
            return await Save();
        }
    }
}