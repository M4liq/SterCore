using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class ExplicitLeaveTypesVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Domyślna liczba Dni)")]
        [Range(1, 25, ErrorMessage = "Wprowadź popdawną domyslna liczbe dnie")]
        public int Limit { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
        public bool IsExplicit { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
    }
}
