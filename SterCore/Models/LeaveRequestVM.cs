using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveRequestVM
    {
      
        public int Id { get; set; }
        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
        public EmployeeVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        public string Comment { get; set; }
    }

    public class AdministratorLeaveRequestVM
    {   
        [Display(Name = "Wszystkie wnioski")]
        public int TotalRequests { get; set; }
        [Display(Name = "Oczekujące wnioski")]
        public int PendingRequests { get; set; }
        [Display(Name = "Odrzucone wnioski")]
        public int RejectedRequests { get; set; }
        [Display(Name = "Zaakceptowane wnioski")]
        public int ApprovedRequests { get; set; } 
        public List<LeaveRequestVM> LeaveRequests { get; set; } 

    }

    public class CreateLeaveRequestVM
    {   
        [Display(Name = "Start Date")]
        [Required]
        [DataType(DataType.Date)] 
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }

    public class EmployeeLeaveRequestsVM
    {
        public List<LeaveRequestVM> LeaveRequests { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
