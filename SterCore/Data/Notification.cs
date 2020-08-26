using System;
using leave_management.Services.Components.ORI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Notification :OrganizationResurceIdentifier
    {
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("NotificationTypeId")]
        public NotificationType NotificationType { get; set; }
        public int NotificationTypeId { get; set; }
        public DateTime DateOfNotification { get; set; }
        public DateTime DateValidUntil { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
