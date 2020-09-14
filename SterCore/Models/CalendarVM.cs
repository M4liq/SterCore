using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class CalendarVM
    {
        public string ContractEmployeeFullName { get; set; }
        public DateTime ContractDateOfContractAgreement { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public string TrainingCourseEmployeeFullName { get; set; }

        public DateTime TrainingCourseStartDate { get; set; }
        public DateTime TrainingCourseEndDate { get; set; }

        public string NotificationEmployeeFullName { get; set; }
        public DateTime NotificationStartDate { get; set; }
        public DateTime NotificationEndDate { get; set; }

        public string MedicalCheckUpEmployeeFullName { get; set; }
        
        public DateTime MedicalCheckUpStartDate { get; set; }
        public DateTime MedicalCheckUpEndDate { get; set; }
    }
}
