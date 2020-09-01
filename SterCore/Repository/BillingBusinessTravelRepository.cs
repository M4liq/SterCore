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
    public class BillingBusinessTravelRepository : IBillingBusinessTravelRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<BillingBusinessTravel> _organizationManager;
        public BillingBusinessTravelRepository(ApplicationDbContext db, IOrganizationResourceManager<BillingBusinessTravel> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(BillingBusinessTravel entity)
        {
            _organizationManager.SetAccess(entity);
            await _db.billingBusinessTravels.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BillingBusinessTravel entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }

            _db.billingBusinessTravels.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<BillingBusinessTravel>> FindAll()
        {
            var BillingBusinessTravel = _organizationManager.FilterDbSetByView(_db.billingBusinessTravels);
            return await BillingBusinessTravel.Include(q => q.BusinessTravel).ToListAsync(); 
        }

        public async Task<BillingBusinessTravel> FindById(int id)
        {
            var BillingBusinessTravel = _organizationManager.FilterDbSetByView(_db.billingBusinessTravels);
            return await BillingBusinessTravel.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public void SetToken(BillingBusinessTravel entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }

        public async Task<bool> Update(BillingBusinessTravel entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.billingBusinessTravels.Update(entity);
            return await Save();
        }
    }
}
