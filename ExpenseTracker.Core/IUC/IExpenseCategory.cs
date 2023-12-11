using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.Utils.FinalResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.IUC
{
    public interface IExpenseCategory
    {
        string title { get; }
        IList<ExpenseCategoryResponseDto> GetAll();
        ServicesResultsDto Add(ExpenseCategoryRequestDto entity);
        ServicesResultsDto Edit(int id, ExpenseCategoryRequestDto entity);
        ServicesResultsDto Delete(int id);
    }
}
