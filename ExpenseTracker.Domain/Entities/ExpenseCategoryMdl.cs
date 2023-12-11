using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class ExpenseCategoryMdl
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isLimitAmount { get; set; }
        public decimal limitAmount { get; set; }
        public DateTime created_in { get; set; }
        public int created_by { get; set; }
    }
}
