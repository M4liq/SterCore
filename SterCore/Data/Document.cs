﻿using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Document : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string DocumentName { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public bool ShowSelectedEmployee { get; set; }
        public bool ShowSelectedDepartment { get; set; }
        public bool ShowCompanyWide { get; set; }
        public string OrganizationToken { get; set; }
        public string DepartmentToken { get; set; }
    }
}
