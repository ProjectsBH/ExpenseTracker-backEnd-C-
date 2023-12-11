using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared.Infrastructure.Settings
{
    public class DataBaseSettings
    {
        public string DBName { get; set; }//expenseTrackerDB_api
        public string SchemaName { get; set; }
        public string ConnectionString { get; set; }
    }
}
