using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace leave_management.Services.ORI.Contracts
{
    public interface IApplicationDataView
    {
        public string OrganizationToken { get; set; }
    }
}
