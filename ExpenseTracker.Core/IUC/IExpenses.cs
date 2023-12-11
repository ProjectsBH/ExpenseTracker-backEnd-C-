using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.Utils.FinalResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.IUC
{
    public interface IExpenses
    {
        string title { get; }
        IList<ExpenseResponseDto> GetTop(int topNo=50);
        ExpenseResponseDto GetBy(long id);
        ServicesResultsDto Add(ExpenseRequestDto entity);
        ServicesResultsDto Edit(long id, ExpenseRequestDto entity);
        ServicesResultsDto Delete(long id);
    }
}
