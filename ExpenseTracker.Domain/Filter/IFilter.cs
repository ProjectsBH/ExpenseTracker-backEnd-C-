using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Filter
{
    public interface IFilter
    {
        string GetWhere();
        void SetFilter(object param);
        object GetFilter();
    }
}
