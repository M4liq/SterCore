using AutoMapper;
using leave_management.Data;
using leave_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap(); 
            CreateMap<LeaveRequests, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequests, AdministratorLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequests, CreateLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveAllocations, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocations, EditLeaveAllocationVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, EditEmployeeVM>().ReverseMap();
            CreateMap<Organization, OrganizationVM>().ReverseMap();
        }
    }
}
