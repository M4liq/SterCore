using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Data.Seeds
{
    public class SeedTransportVehicle : IDataSeed
    {
        public ITransportVehicleRepository transportVehicleRepository { get; }
        public SeedTransportVehicle(ITransportVehicleRepository transportVehicleRepository)
        {
            this.transportVehicleRepository = transportVehicleRepository;
        }
        public void Seed()
        {
            if (transportVehicleRepository.FindAll().Result.Count == 0)
            {
                List<TransportVehicle> entity = new List<TransportVehicle>();
                entity.Add(new TransportVehicle("Samochód prywatny", 1));
                entity.Add(new TransportVehicle("Samochód firmowy", 2));
                entity.Add(new TransportVehicle("Autobus", 3));
                entity.Add(new TransportVehicle("Metro", 3));
                entity.Add(new TransportVehicle("Pociąg", 4));
                entity.Add(new TransportVehicle("Samolot", 5));
                entity.Add(new TransportVehicle("Taxi", 6));
                entity.Add(new TransportVehicle("Rower", 7));

                foreach (var item in entity)
                {
                    var result = transportVehicleRepository.Create(item).Result;
                }
            }
        }
    }
}
