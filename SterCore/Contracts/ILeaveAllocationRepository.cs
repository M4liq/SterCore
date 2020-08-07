using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocations>
    {
        Task<bool> CheckAllocation(int leavetypeid, string employeeid);
        Task<ICollection<LeaveAllocations>> GetLeaveAllocationsByEmployee(string id);
        Task<LeaveAllocations> GetLeaveAllocationsByEmployeeAndType(string id,int leavetypeid);
    }
}
