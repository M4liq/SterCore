using leave_management.Contracts;
using leave_management.Data;
using leave_management.Repository;
using leave_management.Services.Components.ORI;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management
{
    public static class SeedData //Class requires reconstruction 
    {
        public static void Seed(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            IOrganizationRepository organizationRepository,
            IOrganizationResourceManager organizationManager,
            IAuthorizedOrganizationRepository authorizedOrganizationRepository,
            ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUp,
            ICountryRepository countryRepository,
            ICurrencyRepository currencyRepository,
            ITransportVehicleRepository transportVehicleRepository,
            ITypeOfBillingRepository typeOfBillingRepository,
            IConfiguration configuration)
        {
            //Architecture based on events would be better
            SeedRoles(roleManager); 
            SeedUsersAndOrganizations(userManager, organizationManager, organizationRepository, authorizedOrganizationRepository, configuration); //setting up both Users and Organizations is very messy
            SeedMedicalCheckUpTypes(typeOfMedicalCheckUp);
            SeedCountry(countryRepository);
            SeedCurrency(currencyRepository);
            SeedTransportVehicle(transportVehicleRepository);
            SeedTypeOfBilling(typeOfBillingRepository);
        }

        private static void  SeedUsersAndOrganizations(
            UserManager<Employee> userManager,
            IOrganizationResourceManager organizationManager, 
            IOrganizationRepository organizationRepository, 
            IAuthorizedOrganizationRepository authorizedOrganizationRepository,
            IConfiguration configuration
            )
        { 

            if(userManager.FindByNameAsync("admin@stercore.pl").Result == null) 
            {

                var organizationToken = organizationManager.GenerateToken();

                var initalAuthorizedOrganization = new AuthorizedOrganizations
                {
                    AuthorizedOrganizationToken = organizationToken
                };

                var successAuthOrg = authorizedOrganizationRepository.Create(initalAuthorizedOrganization).Result;

                var initalOrganization = new Organization 
                {
                    Name = "Westapp",
                    Code = "WST1",
                    TaxId = "5751900764",
                    Street = "Wiśniowa",
                    HouseNumber = "11",
                    City = "Lubliniec",   
                };


                var successOrg = organizationRepository.Create(initalOrganization, organizationToken).Result;


                if (successOrg)
                {
                    var user = new Employee
                    {
                        UserName = "admin@stercore.pl",
                        Email = "admin@stercore.pl",
                        OrganizationId = initalOrganization.Id,
                        OrganizationToken = organizationToken, //adding organization token, cause it is not handled by user manager
                        ChangedPassword = true
                    };

                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                    {
                       var result = userManager.CreateAsync(user, configuration["AdministratorPSWD"]).Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }
                
                    else
                    {
                        var result = userManager.CreateAsync(user, "P@ssword1").Result;
                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Administrator").Wait();
                        }
                    }    


                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Agent").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Agent"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employer").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Employer"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedMedicalCheckUpTypes(ITypeOfMedicalCheckUpRepository typeOfMedicalCheckUp)
        {
            if (typeOfMedicalCheckUp.FindAll().Result.Count==0)
            {
                List<TypeOfMedicalCheckUp> entity = new List<TypeOfMedicalCheckUp>();
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie wstępne", 200));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie okresowe", 201));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie kontrolne", 202));
                entity.Add(new TypeOfMedicalCheckUp("Badanie lekarskie końcowe", 203));
                entity.Add(new TypeOfMedicalCheckUp("Badanie psychotechniczne", 204));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne terminowe", 205));
                entity.Add(new TypeOfMedicalCheckUp("Badanie sanitarno-epidemiologiczne bezterminowe", 206));
                foreach (var item in entity)
                {
                    var result = typeOfMedicalCheckUp.Create(item).Result;
                }
            }
        }
        private static void SeedCountry(ICountryRepository countryRepository)
        {
            if (countryRepository.FindAll().Result.Count == 0)
            {
                List<Country> entity = new List<Country>();
                entity.Add(new Country("Afganistan", 1));
                entity.Add(new Country("Albania", 2));
                entity.Add(new Country("Algieria", 3));
                entity.Add(new Country("Andora", 4));
                entity.Add(new Country("Angola", 5));
                entity.Add(new Country("Antigua i Barbuda", 6));
                entity.Add(new Country("Arabia Saudyjska", 7));
                entity.Add(new Country("Argentyna", 8));
                entity.Add(new Country("Armenia", 9));
                entity.Add(new Country("Australia", 10));
                entity.Add(new Country("Austria", 11));
                entity.Add(new Country("Azerbejdżan", 12));
                entity.Add(new Country("Bahamy", 13));
                entity.Add(new Country("Bahrajn", 14));
                entity.Add(new Country("Bangladesz", 15));
                entity.Add(new Country("Barbados", 16));
                entity.Add(new Country("Belgia", 17));
                entity.Add(new Country("Belize", 18));
                entity.Add(new Country("Benin", 19));
                entity.Add(new Country("Bhutan", 20));
                entity.Add(new Country("Białoruś", 21));
                entity.Add(new Country("Boliwia", 22));
                entity.Add(new Country("Bośnia i Hercegowina", 23));
                entity.Add(new Country("Botswana", 24));
                entity.Add(new Country("Brazylia", 25));
                entity.Add(new Country("Brunei", 26));
                entity.Add(new Country("Bułgaria", 27));
                entity.Add(new Country("Burkina Faso", 28));
                entity.Add(new Country("Burundi", 29));
                entity.Add(new Country("Chile", 30));
                entity.Add(new Country("Chiny", 31));
                entity.Add(new Country("Chorwacja", 32));
                entity.Add(new Country("Cypr", 33));
                entity.Add(new Country("Czad", 34));
                entity.Add(new Country("Czarnogóra", 35));
                entity.Add(new Country("Czechy", 36));
                entity.Add(new Country("Dania", 37));
                entity.Add(new Country("Demokratyczna Republika Konga", 38));
                entity.Add(new Country("Dominika", 39));
                entity.Add(new Country("Dominikana", 40));
                entity.Add(new Country("Dżibuti", 41));
                entity.Add(new Country("Egipt", 42));
                entity.Add(new Country("Ekwador", 43));
                entity.Add(new Country("Erytrea", 44));
                entity.Add(new Country("Estonia", 45));
                entity.Add(new Country("Eswatini", 46));
                entity.Add(new Country("Etiopia", 47));
                entity.Add(new Country("Fidżi", 48));
                entity.Add(new Country("Filipiny", 49));
                entity.Add(new Country("Finlandia", 50));
                entity.Add(new Country("Francja", 51));
                entity.Add(new Country("Gabon", 52));
                entity.Add(new Country("Gambia", 53));
                entity.Add(new Country("Ghana", 54));
                entity.Add(new Country("Grecja", 55));
                entity.Add(new Country("Grenada", 56));
                entity.Add(new Country("Gruzja", 57));
                entity.Add(new Country("Gujana", 58));
                entity.Add(new Country("Gwatemala", 59));
                entity.Add(new Country("Gwinea", 60));
                entity.Add(new Country("Gwinea Bissau", 61));
                entity.Add(new Country("Gwinea Równikowa", 62));
                entity.Add(new Country("Haiti", 63));
                entity.Add(new Country("Hiszpania", 64));
                entity.Add(new Country("Holandia", 65));
                entity.Add(new Country("Honduras", 66));
                entity.Add(new Country("Indie", 67));
                entity.Add(new Country("Indonezja", 68));
                entity.Add(new Country("Irak", 69));
                entity.Add(new Country("Iran", 70));
                entity.Add(new Country("Irlandia", 71));
                entity.Add(new Country("Islandia", 72));
                entity.Add(new Country("Izrael", 73));
                entity.Add(new Country("Jamajka", 74));
                entity.Add(new Country("Japonia", 75));
                entity.Add(new Country("Jemen", 76));
                entity.Add(new Country("Jordania", 77));
                entity.Add(new Country("Kambodża", 78));
                entity.Add(new Country("Kamerun", 79));
                entity.Add(new Country("Kanada", 80));
                entity.Add(new Country("Katar", 81));
                entity.Add(new Country("Kazachstan", 82));
                entity.Add(new Country("Kenia", 83));
                entity.Add(new Country("Kirgistan", 84));
                entity.Add(new Country("Kiribati", 85));
                entity.Add(new Country("Kolumbia", 86));
                entity.Add(new Country("Komory", 87));
                entity.Add(new Country("Kongo", 88));
                entity.Add(new Country("Korea Południowa", 89));
                entity.Add(new Country("Korea Północna", 90));
                entity.Add(new Country("Kostaryka", 91));
                entity.Add(new Country("Kuba", 92));
                entity.Add(new Country("Kuwejt", 93));
                entity.Add(new Country("Laos", 94));
                entity.Add(new Country("Lesotho", 95));
                entity.Add(new Country("Liban", 96));
                entity.Add(new Country("Liberia", 97));
                entity.Add(new Country("Libia", 98));
                entity.Add(new Country("Liechtenstein", 99));
                entity.Add(new Country("Litwa", 100));
                entity.Add(new Country("Luksemburg", 101));
                entity.Add(new Country("Łotwa", 102));
                entity.Add(new Country("Macedonia Północna", 103));
                entity.Add(new Country("Madagaskar", 104));
                entity.Add(new Country("Malawi", 105));
                entity.Add(new Country("Malediwy", 106));
                entity.Add(new Country("Malezja", 107));
                entity.Add(new Country("Mali", 108));
                entity.Add(new Country("Malta", 109));
                entity.Add(new Country("Maroko", 110));
                entity.Add(new Country("Mauretania", 111));
                entity.Add(new Country("Mauritius", 112));
                entity.Add(new Country("Meksyk", 113));
                entity.Add(new Country("Mikronezja", 114));
                entity.Add(new Country("Mjanma", 115));
                entity.Add(new Country("Mołdawia", 116));
                entity.Add(new Country("Monako", 117));
                entity.Add(new Country("Mongolia", 118));
                entity.Add(new Country("Mozambik", 119));
                entity.Add(new Country("Namibia", 120));
                entity.Add(new Country("Nauru", 121));
                entity.Add(new Country("Nepal", 122));
                entity.Add(new Country("Niemcy", 123));
                entity.Add(new Country("Niger", 124));
                entity.Add(new Country("Nigeria", 125));
                entity.Add(new Country("Nikaragua", 126));
                entity.Add(new Country("Norwegia", 127));
                entity.Add(new Country("Nowa Zelandia", 128));
                entity.Add(new Country("Oman", 129));
                entity.Add(new Country("Pakistan", 130));
                entity.Add(new Country("Palau", 131));
                entity.Add(new Country("Panama", 132));
                entity.Add(new Country("Papua-Nowa Gwinea", 133));
                entity.Add(new Country("Paragwaj", 134));
                entity.Add(new Country("Peru", 135));
                entity.Add(new Country("Polska", 136));
                entity.Add(new Country("Południowa Afryka", 137));
                entity.Add(new Country("Portugalia", 138));
                entity.Add(new Country("Republika Środkowoafrykańska", 139));
                entity.Add(new Country("Republika Zielonego Przylądka", 140));
                entity.Add(new Country("Rosja", 141));
                entity.Add(new Country("Rumunia", 142));
                entity.Add(new Country("Rwanda", 143));
                entity.Add(new Country("Saint Kitts i Nevis", 144));
                entity.Add(new Country("Saint Lucia", 145));
                entity.Add(new Country("Saint Vincent i Grenadyny", 146));
                entity.Add(new Country("Salwador", 147));
                entity.Add(new Country("Samoa", 148));
                entity.Add(new Country("San Marino", 149));
                entity.Add(new Country("Senegal", 150));
                entity.Add(new Country("Serbia", 151));
                entity.Add(new Country("Seszele", 152));
                entity.Add(new Country("Sierra Leone", 153));
                entity.Add(new Country("Singapur", 154));
                entity.Add(new Country("Słowacja", 155));
                entity.Add(new Country("Słowenia", 156));
                entity.Add(new Country("Somalia", 157));
                entity.Add(new Country("Sri Lanka", 158));
                entity.Add(new Country("Stany Zjednoczone", 159));
                entity.Add(new Country("Sudan", 160));
                entity.Add(new Country("Sudan Południowy", 161));
                entity.Add(new Country("Surinam", 162));
                entity.Add(new Country("Syria", 163));
                entity.Add(new Country("Szwajcaria", 164));
                entity.Add(new Country("Szwecja", 165));
                entity.Add(new Country("Tadżykistan", 166));
                entity.Add(new Country("Tajlandia", 167));
                entity.Add(new Country("Tanzania", 168));
                entity.Add(new Country("Timor Wschodni", 169));
                entity.Add(new Country("Togo", 170));
                entity.Add(new Country("Tonga", 171));
                entity.Add(new Country("Trynidad i Tobago", 172));
                entity.Add(new Country("Tunezja", 173));
                entity.Add(new Country("Turcja", 174));
                entity.Add(new Country("Turkmenistan", 175));
                entity.Add(new Country("Tuvalu", 176));
                entity.Add(new Country("Uganda", 177));
                entity.Add(new Country("Ukraina", 178));
                entity.Add(new Country("Urugwaj", 179));
                entity.Add(new Country("Uzbekistan", 180));
                entity.Add(new Country("Vanuatu", 181));
                entity.Add(new Country("Watykan", 182));
                entity.Add(new Country("Wenezuela", 183));
                entity.Add(new Country("Węgry", 184));
                entity.Add(new Country("Wielka Brytania", 185));
                entity.Add(new Country("Wietnam", 186));
                entity.Add(new Country("Włochy", 187));
                entity.Add(new Country("Wybrzeże Kości Słoniowej", 188));
                entity.Add(new Country("Wyspy Marshalla", 189));
                entity.Add(new Country("Wyspy Salomona", 190));
                entity.Add(new Country("Wyspy Świętego Tomasza i Książęca", 191));
                entity.Add(new Country("Zambia", 192));
                entity.Add(new Country("Zimbabwe", 193));
                entity.Add(new Country("Zjednoczone Emiraty Arabskie", 194));
                foreach (var item in entity)
                {
                    var result = countryRepository.Create(item).Result;
                }
            }
        }
        private static void SeedCurrency(ICurrencyRepository currencyRepository)
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
        private static void SeedTransportVehicle(ITransportVehicleRepository transportVehicleRepository)
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
        private static void SeedTypeOfBilling(ITypeOfBillingRepository typeOfBillingRepository)
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
