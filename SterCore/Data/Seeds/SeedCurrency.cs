using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_management.Data.Seeds
{
    public class SeedCurrency : IDataSeed
    {
        public ICurrencyRepository currencyRepository { get; }
        public SeedCurrency(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }
        public void Seed()
        {
            if (currencyRepository.FindAll().Result.Count == 0)
            {
                List<Currency> entity = new List<Currency>();
                entity.Add(new Currency("ALL", 1));
                entity.Add(new Currency("AFN", 2));
                entity.Add(new Currency("ARS", 3));
                entity.Add(new Currency("AWG", 4));
                entity.Add(new Currency("AUD", 5));
                entity.Add(new Currency("AZN", 6));
                entity.Add(new Currency("BSD", 7));
                entity.Add(new Currency("BBD", 8));
                entity.Add(new Currency("BYN", 9));
                entity.Add(new Currency("BZD", 10));
                entity.Add(new Currency("BMD", 11));
                entity.Add(new Currency("BOB", 12));
                entity.Add(new Currency("BAM", 13));
                entity.Add(new Currency("BWP", 14));
                entity.Add(new Currency("BGN", 15));
                entity.Add(new Currency("BRL", 16));
                entity.Add(new Currency("BND", 17));
                entity.Add(new Currency("KHR", 18));
                entity.Add(new Currency("CAD", 19));
                entity.Add(new Currency("KYD", 20));
                entity.Add(new Currency("CLP", 21));
                entity.Add(new Currency("CNY", 22));
                entity.Add(new Currency("COP", 23));
                entity.Add(new Currency("CRC", 24));
                entity.Add(new Currency("HRK", 25));
                entity.Add(new Currency("CUP", 26));
                entity.Add(new Currency("CZK", 27));
                entity.Add(new Currency("DKK", 28));
                entity.Add(new Currency("DOP", 29));
                entity.Add(new Currency("XCD", 30));
                entity.Add(new Currency("EGP", 31));
                entity.Add(new Currency("SVC", 32));
                entity.Add(new Currency("EUR", 33));
                entity.Add(new Currency("FKP", 34));
                entity.Add(new Currency("FJD", 35));
                entity.Add(new Currency("GHS", 36));
                entity.Add(new Currency("GIP", 37));
                entity.Add(new Currency("GTQ", 38));
                entity.Add(new Currency("GGP", 39));
                entity.Add(new Currency("GYD", 40));
                entity.Add(new Currency("HNL", 41));
                entity.Add(new Currency("HKD", 42));
                entity.Add(new Currency("HUF", 43));
                entity.Add(new Currency("ISK", 44));
                entity.Add(new Currency("INR", 45));
                entity.Add(new Currency("IDR", 46));
                entity.Add(new Currency("IRR", 47));
                entity.Add(new Currency("IMP", 48));
                entity.Add(new Currency("ILS", 49));
                entity.Add(new Currency("JMD", 50));
                entity.Add(new Currency("JPY", 51));
                entity.Add(new Currency("JEP", 52));
                entity.Add(new Currency("KZT", 53));
                entity.Add(new Currency("KPW", 54));
                entity.Add(new Currency("KRW", 55));
                entity.Add(new Currency("KGS", 56));
                entity.Add(new Currency("LAK", 57));
                entity.Add(new Currency("LBP", 58));
                entity.Add(new Currency("LRD", 59));
                entity.Add(new Currency("MKD", 60));
                entity.Add(new Currency("MYR", 61));
                entity.Add(new Currency("MUR", 62));
                entity.Add(new Currency("MXN", 63));
                entity.Add(new Currency("MNT", 64));
                entity.Add(new Currency("MZN", 65));
                entity.Add(new Currency("NAD", 66));
                entity.Add(new Currency("NPR", 67));
                entity.Add(new Currency("ANG", 68));
                entity.Add(new Currency("NZD", 69));
                entity.Add(new Currency("NIO", 70));
                entity.Add(new Currency("NGN", 71));
                entity.Add(new Currency("NOK", 72));
                entity.Add(new Currency("OMR", 73));
                entity.Add(new Currency("PKR", 74));
                entity.Add(new Currency("PAB", 75));
                entity.Add(new Currency("PYG", 76));
                entity.Add(new Currency("PEN", 77));
                entity.Add(new Currency("PHP", 78));
                entity.Add(new Currency("PLN", 79));
                entity.Add(new Currency("QAR", 80));
                entity.Add(new Currency("RON", 81));
                entity.Add(new Currency("RUB", 82));
                entity.Add(new Currency("SHP", 83));
                entity.Add(new Currency("SAR", 84));
                entity.Add(new Currency("RSD", 85));
                entity.Add(new Currency("SCR", 86));
                entity.Add(new Currency("SGD", 87));
                entity.Add(new Currency("SBD", 88));
                entity.Add(new Currency("SOS", 89));
                entity.Add(new Currency("ZAR", 90));
                entity.Add(new Currency("LKR", 91));
                entity.Add(new Currency("SEK", 92));
                entity.Add(new Currency("CHF", 93));
                entity.Add(new Currency("SRD", 94));
                entity.Add(new Currency("SYP", 95));
                entity.Add(new Currency("TWD", 96));
                entity.Add(new Currency("THB", 97));
                entity.Add(new Currency("TTD", 98));
                entity.Add(new Currency("TRY", 99));
                entity.Add(new Currency("TVD", 100));
                entity.Add(new Currency("UAH", 101));
                entity.Add(new Currency("GBP", 102));
                entity.Add(new Currency("USD", 103));
                entity.Add(new Currency("UYU", 104));
                entity.Add(new Currency("UZS", 105));
                entity.Add(new Currency("VEF", 106));
                entity.Add(new Currency("VND", 107));
                entity.Add(new Currency("YER", 108));
                entity.Add(new Currency("ZWD", 109));

                foreach (var item in entity)
                {
                    var result = currencyRepository.Create(item).Result;
                }
            }
        }
    }
}
