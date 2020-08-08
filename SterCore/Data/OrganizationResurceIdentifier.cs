using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class OrganizationResurceIdentifier
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrganizationResurceId")]
        public OrganizationResurce OrganizationResurce { get; set; }
        public int OrganizationResurceId { get; set; }
    }
}
