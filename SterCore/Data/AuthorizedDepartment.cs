using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class AuthorizedDepartment
    {
        [Key]
        public int Id { get; set; }
        public string AuthorizedDepartmentToken { get; set; }
    }
}
