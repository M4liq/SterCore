using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class NotificationTypeVM
    {
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}
