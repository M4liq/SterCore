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

        public async Task<bool> Create(Employee entity)
        {
            await _db.Employees.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Employee entity)
        {
            _db.Employees.Remove(entity); 
            return await Save();
        }

        public async Task<bool> Exists(string id)
        {
            var exists = await _db.Employees.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<ICollection<Employee>> FindAll()
        {
            var employees = await _db.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> FindById(string id)
        {
            var employee = await _db.Employees
            .FirstOrDefaultAsync(q => q.Id == id);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithSameOrigin(Organization organization)
        {
            var employees = await _db.Employees.Include(q => q.Organization).ToListAsync(); 
            return employees.Where(q => q.OrganizationId == organization.Id);
        }

        public async Task<Employee> GetUserWithOrganizationByUserId(string id)
        {
            var employee = await _db.Employees.Include(q => q.Organization).FirstOrDefaultAsync(q => q.Id == id);
            return employee;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Employee entity)
        {
            _db.Employees.Update(entity);
            return await Save();
        }
    }
}
