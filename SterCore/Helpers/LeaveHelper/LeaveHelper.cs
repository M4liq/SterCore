using leave_management.Services.Extensions;
using leave_management.Services.LeaveHelper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Helpers.Enums;

namespace leave_management.Services.LeaveHelper
{
    public class LeaveHelper : ILeaveHelper
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveHelper(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public int CountLeaveDays(DateTime from, DateTime to)
        {
            var numberOfDays = 0;
            foreach (DateTime day in EachDay(from, to))
                if (!day.ExtIsHoliday()) numberOfDays++;

            return numberOfDays;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
