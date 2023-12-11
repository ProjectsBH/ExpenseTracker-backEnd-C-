using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.DRY
{
    public class SessionData
    {
        public static int idUserMy { get; set; } = 1;
        public static string? fullNameMy { get; set; }
        public static void DataClear()
        {
            //idUserMy = 0;
            fullNameMy = null;
        }
    }
}
