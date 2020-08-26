using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class CompetenceTypeVM
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa kompetencji")]
        public string name { get; set; }
        public string OrganizationToken { get; set; }
    }
}
