using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data.Seeds
{
    public class SeedMedicalCheckUpTypes : IDataSeed
    {
        public ITypeOfMedicalCheckUpRepository TypeOfMedicalCheckUp { get; }
        public SeedMedicalCheckUpTypes(ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUp)
        {
            this.TypeOfMedicalCheckUp = typeOfMedicalCheckUp;
        }
        public void Seed()
        {
            if (TypeOfMedicalCheckUp.FindAll().Result.Count == 0)
            {
                List<TypeOfMedicalCheckUp> entity = new List<TypeOfMedicalCheckUp>();
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie wstępne", 200));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie okresowe", 201));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie kontrolne", 202));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie końcowe", 203));
                entity.Add(new TypeOfMedicalCheckUp("Badanie psychotechniczne", 204));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne terminowe", 205));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne bezterminowe", 206));
                foreach (var item in entity)
                {
                    var result = TypeOfMedicalCheckUp.Create(item).Result;
                }
            }
        }
    }
}
