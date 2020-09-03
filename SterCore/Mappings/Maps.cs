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
            CreateMap<Organization, OrganizationsVM>().ReverseMap();
            CreateMap<BusinessTravel, BusinessTravelVM>().ReverseMap();
            CreateMap<BusinessTravel, CreateBusinessTravelVM>().ReverseMap();
            CreateMap<BillingBusinessTravel, BillingBusinessTravelVM>().ReverseMap();
            CreateMap<BillingBusinessTravel, CreateBillingBusinessTravelVM>().ReverseMap();
            CreateMap<MedicalCheckUp, MedicalCheckUpVM>().ReverseMap();
            CreateMap<MedicalCheckUp, CreateMedicalCheckUpVM>().ReverseMap();
            CreateMap<MedicalCheckUp, EditMedicalCheckUpVM>().ReverseMap();
            CreateMap<Document, DocumentVM >().ReverseMap();
            CreateMap<Document, CreateDocumentVM >().ReverseMap();
            CreateMap<Competence, CompetenceVM >().ReverseMap();
            CreateMap<Competence, CreateCompetenceVM >().ReverseMap();
            CreateMap<CompetenceType, CompetenceTypeVM>().ReverseMap();
            CreateMap<Expense, ExpenseVM>().ReverseMap();
            CreateMap<Notification, CreateNotificationVM>().ReverseMap();
            CreateMap<Notification, NotificationVM>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeVM>().ReverseMap();
            CreateMap<TrainingCourse, TrainingCourseVM>().ReverseMap();
            CreateMap<TrainingCourse, CreateTrainingCourseVM>().ReverseMap();
            CreateMap<TrainingCourseType, TrainingCourseTypeVM>().ReverseMap();
            CreateMap<Contract, ContractVM>().ReverseMap();
            CreateMap<Contract, CreateContractVM>().ReverseMap();
            CreateMap<ContractType, ContractTypeVM>().ReverseMap();
            CreateMap<Expense, CreateExpenseVM>().ReverseMap();
            CreateMap<Expense, CreateExpenseVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Resource, ResourceVM>().ReverseMap();
            CreateMap<ResourceType, ResourceTypeVM>().ReverseMap();
        }
    }
}
