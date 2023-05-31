using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceReservationSystem.Infrastructure
{
    public class DateTimeMapper
    {
        private static PersianCalendar _persianCalendar;

        static DateTimeMapper() 
        {
            _persianCalendar = new PersianCalendar();
        }
        public static DateTime GetDateTimeFromShamsiDate(string shamsiDate) 
        {
            shamsiDate = ReplacePersianDigits(shamsiDate);
            var splited = shamsiDate.Split("/");
            if (splited.Length < 3)
                throw new Exception("Input date is not in correct format!");
            return _persianCalendar.ToDateTime(Convert.ToInt32(splited[0]), Convert.ToInt32(splited[1]), Convert.ToInt32(splited[2]), 0, 0, 0,0);
        }

        public static TimeSpan GetTime(string time)
        {
            time = ReplacePersianDigits(time);
            time = time.Replace("ق ظ", "");
            time = time.Replace("ب ظ", "");
            time = time.Replace("AM", "");
            time = time.Replace("PM", "");
            time = time.Trim();
            var splited = time.Split(":");
            if (splited.Length < 2)
                throw new Exception("Input time is not in correct format!");
            var result = new TimeSpan(Convert.ToInt32(splited[0]), Convert.ToInt32(splited[1]) , 0);
            return result;
        }

        public static string GetShamsiDate(DateTime date)
        {
            return $"{_persianCalendar.GetYear(date)}/{_persianCalendar.GetMonth(date):D2}/{_persianCalendar.GetDayOfMonth(date):D2}";
        }

        public static string GetTimeString(TimeSpan time)
        {
            return $"{time.Hours:D2}:{time.Minutes:D2}";
        }

        private static string ReplacePersianDigits(string dateString) 
        {
            dateString = dateString.Replace('۰', '0');
            dateString = dateString.Replace('۱', '1');
            dateString = dateString.Replace('۲', '2');
            dateString = dateString.Replace('۳', '3');
            dateString = dateString.Replace('۴', '4');
            dateString = dateString.Replace('۵', '5');
            dateString = dateString.Replace('۶', '6');
            dateString = dateString.Replace('۷', '7');
            dateString = dateString.Replace('۸', '8');
            dateString = dateString.Replace('۹', '9');
            return dateString;

        }

        
    }
}
