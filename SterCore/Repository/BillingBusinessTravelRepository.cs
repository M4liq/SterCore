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
        private readonly IOrganizationResourceManager _organizationManager;
        public BillingBusinessTravelRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(BillingBusinessTravel entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            await _db.billingBusinessTravels.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BillingBusinessTravel entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.billingBusinessTravels.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.billingBusinessTravels
                //ORI Filtring leave types by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<BillingBusinessTravel>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();


            var BillingBusinessTravel = await _db.billingBusinessTravels
                .Include(q => q.Employee)
                .Include(q => q.BusinessTravel)
                .Where(q => q.OrganizationToken == organizationToken)
                .ToListAsync();
            return BillingBusinessTravel;
        }

        public async Task<BillingBusinessTravel> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var BillingBusinessTravel = await _db.billingBusinessTravels
                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .FirstOrDefaultAsync(q => q.Id == id);
            return BillingBusinessTravel;
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
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.billingBusinessTravels.Update(entity);
            return await Save();
        }
    }
}
