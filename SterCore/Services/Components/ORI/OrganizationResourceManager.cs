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

        public OrganizationResourceManager(IHttpContextAccessor session)
        {
            _session = session;
        }

        public string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }
            
        public string GetOrganizationToken()
        {
            return _session.ExtGet<string>("organizationToken");
        }

    }
}
