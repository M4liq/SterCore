using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Components
{
    public class OrganizationResourceManager : IOrganizationResourceManager
    {
        private readonly IHttpContextAccessor _session;
        private readonly ApplicationDbContext _db;

        public OrganizationResourceManager(IHttpContextAccessor session, ApplicationDbContext db)
        {
            _session = session;
            _db = db;
        }

        public async Task<AuthorizedOrganizations> Authorize(string ogranizationToken)
        {
            var authorize = new AuthorizedOrganizations
            {
                AuthorizedOrganizationToken = ogranizationToken
            };

            //requires to save
            await _db.AuthorizedOrganizations.AddAsync(authorize);
            return authorize;
    }

        //OrganizationrResource Interface
        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

        public async Task<int> GetAuthorizedOrganizationId(string token)
        {
            var authorizedOrganization = 
                await _db.AuthorizedOrganizations
                .Where(q => q.AuthorizedOrganizationToken == token)
                .FirstOrDefaultAsync();
            
         //In case authorizaton is not set
            if (authorizedOrganization == null)
                return -1;

            return authorizedOrganization.Id ;
        }

        public async Task<Organization> GetCurrentOrganization()
        {
            var token = GetOrganizationToken();
            var organization = await _db.Organization.Where(q => q.OrganizationToken == token).FirstOrDefaultAsync();
            return organization;
        }

        public string GetOrganizationToken()
        {
            return _session.ExtGet<string>("organizationToken");
        }

        public bool HasPrivilegeGranted()
        {
            if (_session.HttpContext.User.IsInRole("Administrator"))
                return true;

            return false;
        }
    }
}
