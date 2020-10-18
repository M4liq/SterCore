using leave_management.Data;
using leave_management.Services.ORI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ICommonLeaveTypeRepository : IRepositoryBase<CommonLeaveTypes>
    {
        ICollection<CommonLeaveTypes> GetEmployeesByLeaveType(int id);
    }
}
