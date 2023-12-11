using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.DRY
{
    public class MyDateTime
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;//DateTime.UtcNow
        }
    }
}
