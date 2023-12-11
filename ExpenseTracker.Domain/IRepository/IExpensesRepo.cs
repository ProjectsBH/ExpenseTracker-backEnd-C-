using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Domain.IRepository.BaseIRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.IRepository
{
    public interface IExpensesRepo : IBaseAddRepo<ExpensesMdl>, IBaseUpdateRepo<ExpensesMdl>
        , IBaseGetByIdRepo<ExpensesMdl>, IBaseDeleteRepo<long>
    {
        IEnumerable<ExpensesMdl> GetFilter(IFilter filter);
        IEnumerable<ExpensesMdl>  GetTop(int topNo = 50);
        IEnumerable<ExpensesMdl> CheckCategoryIdHasExpenses(int categoryId);
    }
}
