using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.LeaveHelper.Contracts
{
    public interface ILeaveHelper
    {
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru);

        public int CountLeaveDays(DateTime from, DateTime to);
    }
}
