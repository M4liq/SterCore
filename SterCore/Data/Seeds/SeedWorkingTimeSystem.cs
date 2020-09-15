using leave_management.Services.DataSeeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Contracts;


namespace leave_management.Data.Seeds
{
    public class SeedWorkingTimeSystem : IDataSeed
    {
        public IWorkingTimeSystemRepository workingTimeSystemRepository { get; }
        public SeedWorkingTimeSystem(IWorkingTimeSystemRepository workingTimeSystemRepository)
        {
            this.workingTimeSystemRepository = workingTimeSystemRepository;
        }
        public void Seed()
        {
            if (workingTimeSystemRepository.FindAll().Result.Count == 0)
            {
                List<WorkingTimeSystem> entity = new List<WorkingTimeSystem>();
                entity.Add(new WorkingTimeSystem("Podstawowy", 1));
                entity.Add(new WorkingTimeSystem("Równoważny", 2)); 
                foreach (var item in entity)
                {
                    var result = workingTimeSystemRepository.Create(item).Result;
                }
            }
        }
    }
}
