using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Extensions
{
    public static class DataTypeExtensions
    {
        public static bool ExtIsHoliday(this DateTime Day)
        {
            if (Day.DayOfWeek == DayOfWeek.Saturday) return true;
            if (Day.DayOfWeek == DayOfWeek.Sunday) return true;         
            if (Day.Month == 1 && Day.Day == 1) return true; // Nowy Rok
            if (Day.Month == 1 && Day.Day == 6) return true; // Trzech Króli
            if (Day.Month == 5 && Day.Day == 1) return true; // 1 maja
            if (Day.Month == 5 && Day.Day == 3) return true; // 3 maja
            if (Day.Month == 8 && Day.Day == 15) return true; // Wniebowzięcie Najświętszej Marii Panny, Święto Wojska Polskiego
            if (Day.Month == 11 && Day.Day == 1) return true; // Dzień Wszystkich Świętych
            if (Day.Month == 11 && Day.Day == 11) return true; // Dzień Niepodległości 
            if (Day.Month == 12 && Day.Day == 25) return true; // Boże Narodzenie
            if (Day.Month == 12 && Day.Day == 26) return true; // Boże Narodzenie
            int a = Day.Year % 19;
            int b = Day.Year % 4;
            int c = Day.Year % 7;
            int d = (a * 19 + 24) % 30;
            int e = (2 * b + 4 * c + 6 * d + 5) % 7;
            if (d == 29 && e == 6) d -= 7;
            if (d == 28 && e == 6 && a > 10) d -= 7;
            DateTime Easter = new DateTime(Day.Year, 3, 22).AddDays(d + e);
            if (Day.AddDays(-1) == Easter)
                return true; // Wielkanoc (poniedziałek)
            if (Day.AddDays(-60) == Easter)
                return true; // Boże Ciało
            return false;
        }

    }
}
