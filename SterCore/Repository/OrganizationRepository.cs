using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components;
using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager _organizationManager;

        public OrganizationRepository(ApplicationDbContext db, IOrganizationResourceManager organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;

        }

        public async Task<bool> Create(Organization entity)
        {
            //ORI separating data beetween organizations
            entity.OrganizationToken = _organizationManager.GenerateToken();

            await _db.Organization.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Organization entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.Organization.Remove(entity);
            return await Save();

        }

        public async Task<ICollection<Organization>> FindAll()
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            //ORI Filtring organizations by their tokens to get scope
            var organizations = _db.Organization
                .Where(q => q.OrganizationToken == organizationToken);

            return await organizations.ToListAsync(); ;
        }

        public async Task<Organization> FindById(int id)
        {
            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var organization = _db.Organization

            //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken);
             

            return await organization.FirstOrDefaultAsync(q => q.Id == id); 
        }

        public async Task<bool> Exists(int id)
        {
            var organizationToken = _organizationManager.GetOrganizationToken();
            var exists = await _db.Organization

            //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == id);

            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Organization entity)
        {
            //ORI checking if data is from appropirate organization scope
            if (entity.OrganizationToken != _organizationManager.GetOrganizationToken())
            {
                throw new UnauthorizedAccessException();
            }

            _db.Organization.Update(entity);
            return await Save();
        }

        public void SetToken(Organization entity)
        {
            entity.OrganizationToken = _organizationManager.GetOrganizationToken();
        }
    }
}
