using ExpenseTracker.Core.Utils;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Filter.Expenses
{
    internal class ExpensesBetweenDatesFilter : IFilter
    {
        private Dictionary<string, object> param;
        private string propertyNameCategoryId = PropertyHelper.GetPropertyNameBH((ExpensesMdl v) => v.categoryId);
        private string propertyNameDate = PropertyHelper.GetPropertyNameBH((ExpensesMdl v) => v.theDate);
        private Type type = typeof(ExpensesBetweenDatesFilter);
        public void SetFilter(object _param)
        {
            if (_param.GetType() != typeof(Dictionary<string, object>))
                throw new ArgumentException($"{type} The param is not Dictionary");
            param = (Dictionary<string, object>)_param;
        }
        public object GetFilter()
        {
            return new
            {
                categoryIdParam = param.ContainsKey(propertyNameCategoryId) ? param[propertyNameCategoryId] : default(int),
                formDateParam = param.ContainsKey(propertyNameDate) ? param[propertyNameDate] : 0,
                toDateParam = param.ContainsKey(propertyNameDate) ? param["toDate"] : 0
            };

        }

        public string GetWhere()
        {
            StringBuilder where = new StringBuilder();

            //return $"{propertyNameCategoryId} = @categoryIdParam and {propertyNameDate} Between @formDateParam and  @toDateParam";
            if (param.ContainsKey(propertyNameCategoryId) == true)
                where.Append($"{propertyNameCategoryId} = @categoryIdParam ");
            

            if (param.ContainsKey(propertyNameDate) == true)
            {
                _AndAdd();
                where.Append($" {propertyNameDate} Between @formDateParam and  @toDateParam ");
            }

            return where.ToString();
            void _AndAdd()
            {
                if (AndAdd(where.ToString()))
                    where.Append(" and ");
            }
        }

        bool AndAdd(string str)
        {
            if (string.IsNullOrWhiteSpace(str) == false)
                return true;
            return false;
        }
    }
}
