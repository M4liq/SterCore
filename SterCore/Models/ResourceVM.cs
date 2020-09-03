using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class ResourceVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }

        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeId { get; set; }
        public ResourceTypeVM ResourceType { get; set; }
        [Display(Name = "Nazwa zasobu")]
        public int ResourceTypeId { get; set; }
        [Display(Name = "Data od")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Data do")]
        public DateTime DateUntil { get; set; }
        [Display(Name = "Opis")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Zasób został zwrócony")]
        public bool IsReturned { get; set; }
    }
    public class CreateResourceVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }

        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba")]
        public string EmployeeId { get; set; }
        public ResourceTypeVM ResourceType { get; set; }
        [Display(Name = "Nazwa zasobu")]
        public int ResourceTypeId { get; set; }
        [Display(Name = "Data od")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Data do")]
        public DateTime DateUntil { get; set; }
        [Display(Name = "Opis")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Zasób został zwrócony")]
        public bool IsReturned { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> ResourceTypes { get; set; }

    }
}
