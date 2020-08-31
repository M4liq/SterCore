﻿using leave_management.Contracts;
using leave_management.Services.DataSeeds;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data.Seeds
{
    public class SeedCountry : IDataSeed
    {
        public ICountryRepository countryRepository { get; }
        public SeedCountry(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public void Seed()
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
    }
}