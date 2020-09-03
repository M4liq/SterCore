using leave_management.Contracts;
using leave_management.Services.Components.ORI;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Resource> _organizationManager;
        public ResourceRepository(ApplicationDbContext db, IOrganizationResourceManager<Resource> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Resource entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.Resources.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Resource entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.Resources.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Resource>> FindAll()
        {
            var Resource = _organizationManager.FilterDbSetByView(_db.Resources);
            return await Resource.Include(q => q.Employee).ToListAsync();
        }

        public async Task<Resource> FindById(int id)
        {
            var Resource = _organizationManager.FilterDbSetByView(_db.Resources);
            return await Resource.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Resource entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Resources.Update(entity);
            return await Save();
        }
    }
}
