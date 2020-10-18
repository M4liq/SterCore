using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Helpers.Enums;
using leave_management.Services.DataSeeds;

namespace leave_management.Data.Seeds
{
    public class SeedCommonLeaveTypes : IDataSeed
    {
        private readonly ICommonLeaveTypeRepository _leaveTypeRepository;

        public SeedCommonLeaveTypes(ICommonLeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public void Seed()
        {
            if (_leaveTypeRepository.FindAll().Result.Count != 0)
                return;
            
            var vacationLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.VacationLeave,
                Limit = 20,
                AssignedMonthly = true
            };

            var result = _leaveTypeRepository.Create(vacationLeave).Result;

            var vacationLeaveExperienced = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.VacationLeaveExperienced,
                Limit = 26,
                AssignedMonthly = true
            };

             result = _leaveTypeRepository.Create(vacationLeaveExperienced).Result;

            var requestedLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.RequestedLeave,
                Limit = 4,
                AssignedYearly = true
            };

            result = _leaveTypeRepository.Create(requestedLeave).Result;

            var eventLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.EventLeave,
                Limit = 1,
                AssignedYearly = true
            };

            result = _leaveTypeRepository.Create(eventLeave).Result;

            var childCareLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.ChildCareLeave,
                Limit = 2,
                AssignedYearly = true
            };

            result = _leaveTypeRepository.Create(childCareLeave).Result;

            var seekingJobLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.SeekingJobLeave,
                Limit = 2,
                AssignedYearly = true
            };

            result = _leaveTypeRepository.Create(seekingJobLeave).Result;

            var sickLeaveType = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.SickLeave,
                Limit = 5
            };

            result = _leaveTypeRepository.Create(sickLeaveType).Result;
        }
    }
}
