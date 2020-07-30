using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveRequest>
    {
        Task<bool> CheckAllocation(int leavetypeid, string employeeid);
        Task<ICollection<LeaveRequest>> GetLeaveAllocationsByEmployee(string id);
        Task<LeaveRequest> GetLeaveAllocationsByEmployeeAndType(string id,int leavetypeid);
    }
}
