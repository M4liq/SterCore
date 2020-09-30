using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Data;
using leave_management.Helpers.LeaveHelper.Contracts;

namespace leave_management.Services.LeaveHelper.Contracts
{
    public interface ILeaveHelper
    {
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru);

        public int CountLeaveDays(DateTime from, DateTime to);
        public int DivideByCycle(ILeaveType leaveType);
        public List<DateTime> ListUnavailableDates(DateTime from, DateTime to);
    }
}
