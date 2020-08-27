using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.Components.ORI;


namespace leave_management.Data
{
    public class Expense
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("BusinessTravelId")]
        public BusinessTravel BusinessTravel { get; set; }
        public int BusinessTravelId { get; set; }
    }
}
