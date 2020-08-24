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
            var authorizedOrganization = await _db.AuthorizedOrganizations.Where(q => q.AuthorizedOrganizationToken == token).FirstOrDefaultAsync();
            return authorizedOrganization.Id;
        }

        public string GetOrganizationToken()
        {
            return _session.ExtGet<string>("organizationToken");
        }


    }
}
