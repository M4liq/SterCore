using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using leave_management.Models;
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
    }
}
