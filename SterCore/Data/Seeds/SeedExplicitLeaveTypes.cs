using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;
using leave_management.Helpers.Enums;
using leave_management.Services.DataSeeds;

namespace leave_management.Data.Seeds
{
    public class SeedExplicitLeaveTypes : IDataSeed
    {
        private readonly IExplicitLeaveTypeRepository _leaveTypeRepository;

        public SeedExplicitLeaveTypes(IExplicitLeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public void Seed()
        {   
            if(_leaveTypeRepository.FindAll().Result.Count != 0)
                return;

            var unpaidLeave = new ExplicitLeaveTypes()
            {
                Name = ExplicitLeaveTypesEnum.UnpaidLeave,
                Limit = 0,
                AssignedYearly = true
            };

            var result = _leaveTypeRepository.Create(unpaidLeave).Result;
        }
    }
}
