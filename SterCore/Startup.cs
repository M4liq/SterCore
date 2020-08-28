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

            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
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

            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
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

            //Initializind Data Seeding and Generic List required to handle Seeds
            services.AddScoped<ISeed, Seed>();
            services.AddScoped(typeof(IList<>), typeof(List<>));

            //Place to initialize Data Seeds
            services.AddScoped<IDataSeed, SeedRoles>();
            services.AddScoped<IDataSeed, SeedUsersAndOrganizations>();
            services.AddScoped<IDataSeed, SeedMedicalCheckUpTypes>();

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
