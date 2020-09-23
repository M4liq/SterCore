using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Data;
using Microsoft.AspNetCore.Authorization;

namespace leave_management.Repository
{
    public class AuthorizedDepartmentRepository : IAuthorizedDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorizedDepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(AuthorizedDepartment authorizedDepartment)
        {
            await _db.AuthorizedDepartments.AddAsync(authorizedDepartment);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}
