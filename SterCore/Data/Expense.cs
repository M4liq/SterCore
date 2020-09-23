using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Services.Components.ORI;
using leave_management.Services.ORI.Contracts;



namespace leave_management.Data
{
    public class Expense : IApplicationDataView
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationToken { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("BusinessTravelId")]
        public BusinessTravel BusinessTravel { get; set; }
        public int BusinessTravelId { get; set; }
        public string DepartmentToken { get; set; }
    }
}
