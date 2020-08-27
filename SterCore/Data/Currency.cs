using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.Components.ORI;


namespace leave_management.Data
{
    public class Currency
    {
        public Currency(string name, int value)
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
