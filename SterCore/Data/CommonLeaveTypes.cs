using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Data
{
    public class CommonLeaveTypes 
    {   
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultLimit { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
