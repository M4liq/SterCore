using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Helpers.LeaveHelper.Contracts
{
    public interface ILeaveType
    {
        public bool AssignedYearly { get; set; }
        public bool AssignedMonthly { get; set; }
        public int Limit { get; set; }
    }
}
