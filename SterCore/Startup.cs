using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using leave_management.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using leave_management.Repository;
using leave_management.Contracts;
using AutoMapper;
using leave_management.Mappings;
using System;
using leave_management.Services.Components;
using leave_management.Services.Components.ORI;
using leave_management.Services.Extensions;
using leave_management.Services.DataSeeds;
using leave_management.Services.DataSeeds.Contracts;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using leave_management.Data.Seeds;
using leave_management.Services.LeaveHelper.Contracts;
using leave_management.Services.LeaveHelper;

namespace leave_management
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddCors(o => o.AddPolicy("SchedulerPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
              services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                   Configuration.GetConnectionString("AzureConnection")));
                Environment.SetEnvironmentVariable("ADMINISTRATOR_PASSWORD", Configuration["AdministratorPSWD"]);
                services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();
            }
            else
            {
              services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            }

            //Repositories
            services.AddScoped<ICommonLeaveTypeRepository, CommonLeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAuthorizedOrganizationRepository, AuthorizedOrganizationRepository>();
            services.AddScoped<IOrganizationResourceManager, OrganizationResourceManager>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IBusinessTravelRepository, BusinessTravelRepository>();
            services.AddScoped<IBillingBusinessTravelRepository, BillingBusinessTravelRepository>();
            services.AddScoped<IMedicalCheckUpRepository, MedicalCheckUpRepository>();
            services.AddScoped<ITypeOfMedicalCheckUpRepository, TypeOfMedicalCheckUpRepository>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<ICompetenceRepository, CompetenceRepository>();
            services.AddScoped<ICompetenceTypeRepository, CompetenceTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<ITransportVehicleRepository, TransportVehicleRepository>();
            services.AddScoped<ITypeOfBillingRepository, TypeOfBillingRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
            services.AddScoped<ITrainingCourseTypeRepository, TrainingCourseTypeRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IResourceTypeRepository, ResourceTypeRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IAuthorizedDepartmentRepository, AuthorizedDepartmentRepository>();
            services.AddScoped<IExplicitLeaveTypeRepository, ExplicitLeaveRepository>();
            services.AddScoped<IWorkingTimeSystemRepository, WorkingTimeSystemRepository>();
            services.AddScoped<IWorkTimeScheduleRepository, WorkTimeScheduleRepository>();
            services.AddScoped<IWorkTimeScheduleEmployeesRepository, WorkTimeScheduleEmployeesRepository>();

            //Organization Resource Manager Initialization Fields
            services.AddScoped<IOrganizationResourceManager<LeaveType>, OrganizationResourceManager<LeaveType>>();
            services.AddScoped<IOrganizationResourceManager<Document>, OrganizationResourceManager<Document>>();
            services.AddScoped<IOrganizationResourceManager<Employee>, OrganizationResourceManager<Employee>>();
            services.AddScoped<IOrganizationResourceManager<LeaveAllocations>, OrganizationResourceManager<LeaveAllocations>>();
            services.AddScoped<IOrganizationResourceManager<LeaveRequests>, OrganizationResourceManager<LeaveRequests>>();
            services.AddScoped<IOrganizationResourceManager<BillingBusinessTravel>, OrganizationResourceManager<BillingBusinessTravel>>();
            services.AddScoped<IOrganizationResourceManager<BusinessTravel>, OrganizationResourceManager<BusinessTravel>>();
            services.AddScoped<IOrganizationResourceManager<Competence>, OrganizationResourceManager<Competence>>();
            services.AddScoped<IOrganizationResourceManager<CompetenceType>, OrganizationResourceManager<CompetenceType>>();
            services.AddScoped<IOrganizationResourceManager<Expense>, OrganizationResourceManager<Expense>>();
            services.AddScoped<IOrganizationResourceManager<MedicalCheckUp>, OrganizationResourceManager<MedicalCheckUp>>();
            services.AddScoped<IOrganizationResourceManager<Notification>, OrganizationResourceManager<Notification>>();
            services.AddScoped<IOrganizationResourceManager<NotificationType>, OrganizationResourceManager<NotificationType>>();
            services.AddScoped<IOrganizationResourceManager<TrainingCourse>, OrganizationResourceManager<TrainingCourse>>();
            services.AddScoped<IOrganizationResourceManager<Contract>, OrganizationResourceManager<Contract>>();
            services.AddScoped<IOrganizationResourceManager<Department>, OrganizationResourceManager<Department>>();
            services.AddScoped<IOrganizationResourceManager<Resource>, OrganizationResourceManager<Resource>>();
            services.AddScoped<IOrganizationResourceManager<ResourceType>, OrganizationResourceManager<ResourceType>>();
            services.AddScoped<IOrganizationResourceManager<Application>, OrganizationResourceManager<Application>>();
            services.AddScoped<IOrganizationResourceManager<ExplicitLeaveTypes>, OrganizationResourceManager<ExplicitLeaveTypes>>();
            services.AddScoped<IOrganizationResourceManager<WorkTimeSchedule>, OrganizationResourceManager<WorkTimeSchedule>>();
            services.AddScoped<IOrganizationResourceManager<WorkTimeScheduleEmployee>, OrganizationResourceManager<WorkTimeScheduleEmployee>>();

            //Initializind Data Seeding and Generic List required to handle Seeds
            services.AddScoped<ISeed, Seed>();
            services.AddScoped(typeof(IList<>), typeof(List<>));

            //Place to initialize Data Seeds
            services.AddScoped<IDataSeed, SeedRoles>();
            services.AddScoped<IDataSeed, SeedUsersAndOrganizations>();
            services.AddScoped<IDataSeed, SeedMedicalCheckUpTypes>();
            services.AddScoped<IDataSeed, SeedCountry>();
            services.AddScoped<IDataSeed, SeedCurrency>();
            services.AddScoped<IDataSeed, SeedTransportVehicle>();
            services.AddScoped<IDataSeed, SeedTypeOfBilling>();
            services.AddScoped<IDataSeed, SeedTrainingCourseType>();
            services.AddScoped<IDataSeed, SeedContractType>();

            services.AddScoped<IDataSeed, SeedCommonLeaveTypes>();
            services.AddScoped<IDataSeed, SeedExplicitLeaveTypes>();

            services.AddScoped<IDataSeed, SeedWorkingTimeSystem>();


            //Initializing LeaveHelper 
            services.AddScoped<ILeaveHelper, LeaveHelper>();

            services.AddAutoMapper(typeof(Maps));

            services.AddDefaultIdentity<Employee>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);//You can set Time  
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            ISeed seed
        )
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // app.UseCors(builder => builder
           //    .AllowAnyHeader()
           //    .AllowAnyMethod()
           //    .SetIsOriginAllowed((host) => true)
           //    .AllowCredentials()
           //);
            app.UseHttpsRedirection();
           
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("SchedulerPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
  			app.ExtSeed(seed);             

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
