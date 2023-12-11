using ExpenseTracker.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Filter.Expenses
{
    public class ExpensesCategoryFilter : IFilter
    {
        private int categoryId;
        public void SetFilter(object param)
        {
            categoryId = (int)param;
        }
        public object GetFilter()
        {
            if (categoryId == 0 || categoryId == default(int))
                throw new ArgumentException("ExpensesCategoryFilter : categoryId");
            return new { category = categoryId };
        }

        public string GetWhere()
        {
            return "categoryId = @category";
        }
    }
}
