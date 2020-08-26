using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class AuthorizedOrganizations
    {
        [Key]
        public int Id { get; set; }
        public string AuthorizedOrganizationToken { get; set; }
    }
}
