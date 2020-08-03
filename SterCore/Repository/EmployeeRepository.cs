using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Employee> GetUserWithOrganizationByUserId(string id)
        {
            var employee = await _db.Employees.Include(q => q.Organization).FirstOrDefaultAsync(q => q.Id == id);
            return employee;
        }
    }
}
