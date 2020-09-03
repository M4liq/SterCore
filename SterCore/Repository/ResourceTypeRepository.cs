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
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<ResourceType> _organizationManager;
        public ResourceTypeRepository(ApplicationDbContext db, IOrganizationResourceManager<ResourceType> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(ResourceType entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.ResourceTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(ResourceType entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.ResourceTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<ResourceType>> FindAll()
        {
            var ResourceType = _organizationManager.FilterDbSetByView(_db.ResourceTypes);
            return await ResourceType.ToListAsync();
        }

        public async Task<ResourceType> FindById(int id)
        {
            var ResourceType = _organizationManager.FilterDbSetByView(_db.ResourceTypes);
            return await ResourceType.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(ResourceType entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.ResourceTypes.Update(entity);
            return await Save();
        }
    }
}
