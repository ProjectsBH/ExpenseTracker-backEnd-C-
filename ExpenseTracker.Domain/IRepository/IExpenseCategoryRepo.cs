using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.IRepository.BaseIRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.IRepository
{
    public interface IExpenseCategoryRepo : IBaseAddRepo<ExpenseCategoryMdl>, IBaseUpdateRepo<ExpenseCategoryMdl>
        , IBaseGetByIdRepo<ExpenseCategoryMdl>, IBaseGetAllRepo<ExpenseCategoryMdl>, IBaseDeleteRepo<int>
    {
        ExpenseCategoryMdl GetForExists(string name, object? id = null);
    }
}
