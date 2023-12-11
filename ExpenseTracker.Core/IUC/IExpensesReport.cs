using ExpenseTracker.Core.DTOs.ExpenseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.IUC
{
    public  interface IExpensesReport
    {
        string title { get; }
        IList<ExpenseResponseDto> GetBy(int? categoryId, DateTime? fromDate, DateTime? toDate);
    }
}
