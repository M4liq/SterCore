using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Data.Seeds
{
    public class SeedTypeOfBilling : IDataSeed
    {
        public ITypeOfBillingRepository typeOfBillingRepository { get; }
        public SeedTypeOfBilling(ITypeOfBillingRepository typeOfBillingRepository)
        {
            this.typeOfBillingRepository = typeOfBillingRepository;
        }
        public void Seed()
        {
            if (typeOfBillingRepository.FindAll().Result.Count == 0)
            {
                List<TypeOfBilling> entity = new List<TypeOfBilling>();
                entity.Add(new TypeOfBilling("Zaliczka", 1));
                entity.Add(new TypeOfBilling("Nadpłata firmy", 2));
                entity.Add(new TypeOfBilling("Dopłata pracownika", 3));

                foreach (var item in entity)
                {
                    var result = typeOfBillingRepository.Create(item).Result;
                }
            }
        }
    }
}
