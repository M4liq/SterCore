using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data.Seeds
{
    public class SeedTrainingCourseType : IDataSeed
    {
        public ITrainingCourseTypeRepository trainingCourseType { get; }
        public SeedTrainingCourseType(ITrainingCourseTypeRepository trainingCourseType)
        {
            this.trainingCourseType = trainingCourseType;
        }
        public void Seed()
        {
            if (trainingCourseType.FindAll().Result.Count == 0)
            {
                List<TrainingCourseType> entity = new List<TrainingCourseType>();
                entity.Add(new TrainingCourseType("Szkolenie BHP wstępne", 100));
                entity.Add(new TrainingCourseType("Szkolenie BHP okresowe", 101));
                foreach (var item in entity)
                {
                    var result = trainingCourseType.Create(item).Result;
                }
            }
        }
    }
}
