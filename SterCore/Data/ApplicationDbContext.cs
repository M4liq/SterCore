﻿using System;
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
        public DbSet<CommonLeaveTypes> CommonLeaveTypes { get; set; }
        public DbSet<ExplicitLeaveTypes> ExplicitLeaveTypes { get; set; }
        public DbSet<LeaveAllocations> LeaveAllocations { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<BusinessTravel> BusinessTravel { get; set; }
        public DbSet<BillingBusinessTravel> BillingBusinessTravels { get; set; }
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
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AuthorizedDepartment> AuthorizedDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>()
                .HasMany<Employee>()
                .WithOne(e => e.Department)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Employee>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
