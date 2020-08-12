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
    //Implementing ORI to do
    public class BusinessTravelRepository : IBusinessTravelRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public BusinessTravelRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(BusinessTravel entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();

            await _db.BusinessTravel.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BusinessTravel entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.BusinessTravel.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();

            var exists = await _db.BusinessTravel

                //ORI Filtring leave types by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);

            return exists;
        }

        public async Task<ICollection<BusinessTravel>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            //ORI filtering by token
            var BusinessTravel = await _db.BusinessTravel
                .Where(q => q.OrganizationToken == organizationToken)
                .ToListAsync();
            return BusinessTravel;
        }

        public async Task<BusinessTravel> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var BusinessTravel = await _db.BusinessTravel

                //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .FirstOrDefaultAsync(q => q.Id == id);

            return BusinessTravel;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public void SetToken(BusinessTravel entity)
        {
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = token;
        }

        public async Task<bool> Update(BusinessTravel entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.BusinessTravel.Update(entity);
            return await Save();
        }
    }
}
