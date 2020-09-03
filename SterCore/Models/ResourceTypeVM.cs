using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class ResourceTypeVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Pokaż pole 'Data od'")]
        public bool showDateFrom { get; set; }
        [Display(Name = "Pokaż pole 'Data do'")]
        public bool showDateUntil { get; set; }
        [Display(Name = "Pokaż pole 'Zwrócono'")]
        public bool showReturned { get; set; }
    }
}
