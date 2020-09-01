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
    public class BusinessTravelRepository : IBusinessTravelRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<BusinessTravel> _organizationManager;

        public BusinessTravelRepository(ApplicationDbContext db, IOrganizationResourceManager<BusinessTravel> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(BusinessTravel entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.BusinessTravel.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BusinessTravel entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.BusinessTravel.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<BusinessTravel>> FindAll()
        {
            var BusinessTravel = _organizationManager.FilterDbSetByView(_db.BusinessTravel);
            return await BusinessTravel.Include(q => q.Employee).ToListAsync();
        }

        public async Task<BusinessTravel> FindById(int id)
        {
            var BusinessTravel = _organizationManager.FilterDbSetByView(_db.BusinessTravel);
            return await BusinessTravel.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(BusinessTravel entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.BusinessTravel.Update(entity);
            return await Save();
        }

        public async Task<int> getLatestApplicationId()
        {
            int latestApplicationId = 1;
            int? latestId = _db.BusinessTravel.Max(u => (int?)u.Id);
            if (latestId.HasValue)
            {
                latestApplicationId = Convert.ToInt32(latestId);
            }
            else
            {
                latestApplicationId = 0;
            }
            return  latestApplicationId;
        }
    }
}
