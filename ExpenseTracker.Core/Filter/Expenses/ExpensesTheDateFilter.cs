using ExpenseTracker.Core.Utils;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Filter.Expenses
{
    internal class ExpensesTheDateFilter : IFilter
    {
        private DateTime theDate;
        private string propertyName = PropertyHelper.GetPropertyNameBH((ExpensesMdl v) => v.theDate);
        public void SetFilter(object param)
        {
            if (param.GetType() != typeof(DateTime))
                throw new ArgumentException("ExpensesTheDateFilter The param is not date");
            theDate = Convert.ToDateTime(param);
        }
        public object GetFilter()
        {
            if (theDate == default(DateTime))
                throw new ArgumentException("ExpensesTheDateFilter : theDate");
            //return new { dateParam = theDate };
            return new { formDateParam = theDate.StartOfDay(), toDateParam= theDate.EndOfDay() };
        }

        public string GetWhere()
        {
            //return $"{propertyName} = @dateParam";
            return $"{propertyName} Between @formDateParam and  @toDateParam";
        }
    }
}
