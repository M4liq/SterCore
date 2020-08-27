using leave_management.Services.DataSeeds.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.DataSeeds
{
    public class Seed : ISeed
    {
        private readonly IList<IDataSeed> _dataSeeds;
        public Seed(IList<IDataSeed> dataSeeds)
        {
            _dataSeeds = dataSeeds;
        }

        public void SeedDataBase()
        {
            foreach (var dataSeed in _dataSeeds)
                dataSeed.Seed();
        }

    }
}
