using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Components.ORI
{
        public class OrganizationResurceIdentifier 
        {
            [Key]
            public int Id { get; set; }

            public string OrganizationToken { get; set; }

    }
}

