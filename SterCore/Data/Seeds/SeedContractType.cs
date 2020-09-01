using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data.Seeds
{
    public class SeedContractType : IDataSeed
    {
        public IContractTypeRepository contractType { get; }
        public SeedContractType(IContractTypeRepository contractType)
        {
            this.contractType = contractType;
        }
        public void Seed()
        {
            if (contractType.FindAll().Result.Count == 0)
            {
                List<ContractType> entity = new List<ContractType>();
                entity.Add(new ContractType("Umowa o pracę na czas nieokreślony", 1));
                entity.Add(new ContractType("Umowa o pracę na czas określony", 2));
                entity.Add(new ContractType("Umowa o pracę na okres próbny", 3));
                entity.Add(new ContractType("Umowa o pracę na zastępstwo", 4));
                entity.Add(new ContractType("Umowa B2B", 5));
                entity.Add(new ContractType("Umowa zlecenie", 6));
                entity.Add(new ContractType("Umowa o dzieło", 7));
                entity.Add(new ContractType("Praktyka", 8));
                entity.Add(new ContractType("Praca tymczasowa", 9));
                entity.Add(new ContractType("Aneks", 10));
                foreach (var item in entity)
                {
                    var result = contractType.Create(item).Result;
                }
            }
        }
    }
}
