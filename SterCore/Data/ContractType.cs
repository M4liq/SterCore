﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class ContractType
    {
        public ContractType(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
