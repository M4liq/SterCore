using System;
using System.Collections.Generic;
using System.Linq;
using leave_management.Services.Components.ORI;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Data
{
    public class TransportVehicle
    {
        public TransportVehicle(string name, int value)
        {
            Name = name;
            Value = value;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
