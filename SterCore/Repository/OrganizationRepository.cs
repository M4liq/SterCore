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

        public OrganizationRepository(ApplicationDbContext db, 
            IOrganizationResourceManager organizationManager
            )
        {
            _db = db;
            _organizationManager = organizationManager;

        }

        public async Task<bool> Create(Organization entity)
        {
            //ORI separating data between organizations
            var token = _organizationManager.GetOrganizationToken();
            entity.OrganizationToken = _organizationManager.GenerateToken(); 

            var authorizedOrganizationId = await _organizationManager.GetAuthorizedOrganizationId(token);

            //If our organization is not set as authorized organization our organization must be set as superior organization 
            //for created organization otherwise getting access to created organization would be impossible 
            if (authorizedOrganizationId == -1)
            {
                var authorize = new AuthorizedOrganizations
                {
                    AuthorizedOrganizationToken = token
                };

                entity.AuthorizedOrganizations = authorize;
            }
            else
                entity.AuthorizedOrganizationId = authorizedOrganizationId;

            await _db.Organization.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Create(Organization entity, string organizationToken)
        {

            //ORI separating data beetween organizations 
            //Overloaded method enables creation of organization when ORI can not generate token. Token can be set manuall. Feg in Data seeding
            entity.OrganizationToken = organizationToken;
            entity.AuthorizedOrganizationId = await _organizationManager.GetAuthorizedOrganizationId(organizationToken);

            await _db.Organization.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Organization entity)
        {

            //Allow administrator deleting all organizations 

            if (_organizationManager.HasPrivilegeGranted())
            {
                _db.Organization.Remove(entity);
                return await Save();
            }

            var token = _organizationManager.GetOrganizationToken();
            var validate = await _db.Organization
                .Where(q => q.AuthorizedOrganizations.AuthorizedOrganizationToken == token || q.OrganizationToken == token)
                .AnyAsync(q => q.Id == entity.Id);

            if(validate)
                _db.Organization.Remove(entity);

            return await Save();

        }

        public async Task<ICollection<Organization>> FindAll()
        {
            //Allow administrator accessing all organizations
            if (_organizationManager.HasPrivilegeGranted())
            {
                return await _db.Organization.ToListAsync();
            }

            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            //ORI Filtring leave types by their tokens to get scope
            var organizations = _db.Organization
                .Where(q => q.AuthorizedOrganizations.AuthorizedOrganizationToken == organizationToken || q.OrganizationToken == organizationToken);

            return await organizations.ToListAsync();


        }

        public async Task<Organization> FindById(int id)
        {
            //Allow administrator accessing all organizations
            if (_organizationManager.HasPrivilegeGranted())
            {
                return await _db.Organization.FirstOrDefaultAsync(q => q.Id == id);
            }

            //ORI getting token to find organization scope
            var organizationToken = _organizationManager.GetOrganizationToken();

            var organization = _db.Organization

            //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.AuthorizedOrganizations.AuthorizedOrganizationToken == organizationToken || q.OrganizationToken == organizationToken);
             

            return await organization.FirstOrDefaultAsync(q => q.Id == id); 
        }

        public async Task<bool> Exists(int id)
        {
            //Allow administrator accessing all organizations
            if (_organizationManager.HasPrivilegeGranted())
            {
                return await _db.Organization.AnyAsync(q => q.Id == id); ;
            }

            var organizationToken = _organizationManager.GetOrganizationToken();
            var exists = await _db.Organization

            //ORI Filtring organizations by their tokens to get scope
                .Where(q => q.AuthorizedOrganizations.AuthorizedOrganizationToken == organizationToken || q.OrganizationToken == organizationToken)
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

            if (_organizationManager.HasPrivilegeGranted())
            {
                _db.Organization.Update(entity);
                return await Save();
            }

            var organizationToken = _organizationManager.GetOrganizationToken();
            entity.AuthorizedOrganizationId = await _organizationManager.GetAuthorizedOrganizationId(organizationToken);

            var validate = await _db.Organization
                .Where(q => q.AuthorizedOrganizations.AuthorizedOrganizationToken == organizationToken || q.OrganizationToken == organizationToken)
                .AnyAsync(q => q.Id == entity.Id);

            if(validate)
                _db.Organization.Update(entity);

            return await Save();
        }

    }
}
