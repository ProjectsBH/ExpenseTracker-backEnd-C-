using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        //fromDate = fromDate.StartOfDay();
        //toDate = toDate.EndOfDay();
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay2(this DateTime theDate)
        {
            return theDate.Date;
        }
        public static DateTime EndOfDay2(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }

    }
}
