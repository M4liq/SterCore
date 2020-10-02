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
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public SeedCommonLeaveTypes(ILeaveTypeRepository leaveTypeRepository)
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
                DefaultLimit = 20
            };

            var result = _leaveTypeRepository.Create(vacationLeave).Result;

            var vacationLeaveExperienced = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.VacationLeaveExperienced,
                DefaultLimit = 26
            };

             result = _leaveTypeRepository.Create(vacationLeaveExperienced).Result;

            var requestedLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.RequestedLeave,
                DefaultLimit = 4
            };

            result = _leaveTypeRepository.Create(requestedLeave).Result;

            var eventLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.EventLeave,
                DefaultLimit = 1
            };

            result = _leaveTypeRepository.Create(eventLeave).Result;

            var childCareLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.ChildCareLeave,
                DefaultLimit = 2
            };

            result = _leaveTypeRepository.Create(childCareLeave).Result;

            var seekingJobLeave = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.SeekingJobLeave,
                DefaultLimit = 2
            };

            result = _leaveTypeRepository.Create(seekingJobLeave).Result;

            var sickLeaveType = new CommonLeaveTypes()
            {
                Name = CommonLeaveTypesEnum.SickLeave,
                DefaultLimit = 5
            };

            result = _leaveTypeRepository.Create(sickLeaveType).Result;
        }
    }
}
