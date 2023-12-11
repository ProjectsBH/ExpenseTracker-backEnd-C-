using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.Infrastructure.Settings
{
    public class AppSettings
    {
        public static string AppName { get; set; }
        public static DataBaseProvider DBProvider { get; set; } = DataBaseProvider.SqlServer;
        public static DataBaseSettings DataBaseSettings { get; set; }
    }
    public enum DataBaseProvider
    {
        SqlServer,
        MySQL,
        SQLite,
        Postgresql
    }
}
