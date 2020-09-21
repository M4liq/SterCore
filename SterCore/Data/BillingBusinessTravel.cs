using leave_management.Services.Components.ORI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using leave_management.Services.ORI.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class BillingBusinessTravel : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        [ForeignKey("BusinessTravelId")]
        public BusinessTravel BusinessTravel { get; set; }
        public int BusinessTravelId { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool IsPaid { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("TypeOfBillingId")]
        public TypeOfBilling TypeOfBilling { get; set; }
        public int TypeOfBillingId { get; set; }
        public string DepartmentToken { get; set; }
    }
}
