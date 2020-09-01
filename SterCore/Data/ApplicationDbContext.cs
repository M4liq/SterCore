using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using leave_management.Models;
using System.Net;
using leave_management.Services.Components.ORI;

namespace leave_management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequests> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocations> LeaveAllocations { get; set; }
        public DbSet<Organization> Organization { get; set; }

        public DbSet<BusinessTravel> BusinessTravel { get; set; }
        public DbSet<BillingBusinessTravel> billingBusinessTravels { get; set; }
        public DbSet<TypeOfMedicalCheckUp> TypeOfMedicalCheckUps { get; set; }
        public DbSet<MedicalCheckUp> MedicalCheckUps { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AuthorizedOrganizations> AuthorizedOrganizations { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<CompetenceType> CompetenceTypes { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<TypeOfBilling> TypeOfBillings { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<TrainingCourse> TrainingCourses { get; set; }
        public DbSet<TrainingCourseType> TrainingCourseTypes { get; set; }

    }
}
