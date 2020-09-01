using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class NotificationVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba:")]
        public string EmployeeId { get; set; }
        public NotificationTypeVM NotificationType { get; set; }
        [Display(Name = "Typ powiadomienia:")]
        public int NotificationTypeId { get; set; }
        [Display(Name = "Data:")]
        public DateTime DateOfNotification { get; set; }
        [Display(Name = "Ważne do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }
    }
    public class CreateNotificationVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public EmployeeVM Employee { get; set; }
        [Display(Name = "Osoba:")]
        public string EmployeeId { get; set; }
        public NotificationTypeVM NotificationType { get; set; }
        [Display(Name = "Typ powiadomienia:")]
        public int NotificationTypeId { get; set; }
        [Display(Name = "Data:")]
        public DateTime DateOfNotification { get; set; }
        [Display(Name = "Ważne do:")]
        public DateTime DateValidUntil { get; set; }
        [Display(Name = "Opis:")]
        public string AdditionalInfo { get; set; }
        [Display(Name = "Pokaż pracownikowi")]
        public bool ShowSelectedEmployee { get; set; }
        [Display(Name = "Pokaż przełożonemu")]
        public bool ShowSelectedDepartment { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> NotificationTypes { get; set; }


    }

}
