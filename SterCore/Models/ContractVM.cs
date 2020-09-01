using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class ContractVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public ContractTypeVM ContractType { get; set; }
        [Display(Name = "Typ umowy:")]
        public int ContractTypeId { get; set; }
        [Display(Name = "Data zawarcia:")]
        public DateTime DateOfContractAgreement { get; set; }     
        [Display(Name = "Data od:")]
        public DateTime DateValidFrom { get; set; }
        [Display(Name = "Data do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }
        [Display(Name = "Osoba:")]
        public string EmployeeFullName { get; set; }
        [Display(Name = "Typ umowy:")]
        public string ContractTypeName { get; set; }
    }

    public class CreateContractVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Required]
        [Display(Name = "Osoba:")]
        public string EmployeeId { get; set; }
        public ContractTypeVM ContractType { get; set; }
        [Required]
        [Display(Name = "Typ umowy:")]
        public int ContractTypeId { get; set; }
        [Required]
        [Display(Name = "Data zawarcia:")]
        public DateTime DateOfContractAgreement { get; set; }
        [Required]
        [Display(Name = "Data od:")]
        public DateTime DateValidFrom { get; set; }
        [Display(Name = "Data do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> ContractTypes { get; set; }
    }
}
