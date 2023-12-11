using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class ExpensesMdl
    {
        public long id { get; set; }
        public int categoryId { get; set; }
        public DateTime theDate { get; set; }
        public decimal amount { get; set; }
        public string theStatement { get; set; }
        public DateTime created_in { get; set; }
        public int created_by { get; set; }
    }
}
