using leave_management.Contracts;
using leave_management.Data;
using leave_management.Services.Components.ORI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IOrganizationResourceManager<Expense> _organizationManager;

        public ExpenseRepository(ApplicationDbContext db, IOrganizationResourceManager<Expense> organizationManager)
        {
            _db = db;
            _organizationManager = organizationManager;
        }
        public async Task<bool> Create(Expense entity)
        {

            _organizationManager.SetAccess(entity);
            await _db.Expenses.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Expense entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Expenses.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            if (await FindById(id) == null)
                return false;
            else
                return true;
        }

        public async Task<ICollection<Expense>> FindAll()
        {
            var Expense = _organizationManager.FilterDbSetByView(_db.Expenses);
            return await Expense.ToListAsync();
        }

        public async Task<Expense> FindById(int id)
        {
            var Expense = _organizationManager.FilterDbSetByView(_db.Expenses);
            return await Expense.FirstOrDefaultAsync(q => q.Id == id); ;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Expense entity)
        {
            if (!_organizationManager.VerifyAccess(entity))
            {
                throw new UnauthorizedAccessException();
            }
            _db.Expenses.Update(entity);
            return await Save();
        }
    }
}
