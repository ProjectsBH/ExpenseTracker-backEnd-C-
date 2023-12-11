using ExpenseTracker.Core.Configuration;
using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.Filter.Expenses;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.Utils;
using ExpenseTracker.Core.Utils.FinalResults;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Domain.IRepository;
using ExpenseTracker.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.UC
{
    public class ExpensesReportUC : IExpensesReport
    {
        private readonly IExpensesRepo _repo;
        public ExpensesReportUC(IExpensesRepo repo)
        {
            _repo = repo;
        }
        public string title => "تقرير بالمصروفات";

        public IList<ExpenseResponseDto> GetBy(int? categoryId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                if (categoryId == null && fromDate == null)
                    throw new ArgumentException("الفئة و التاريخ فارغ");
                var items = Get(categoryId, fromDate, toDate).ToList();
                IList<ExpenseResponseDto> entities = new ExpenseResponseDto().fromModel(items);
                return entities;
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }

        private IList<ExpensesMdl> Get(int? categoryId, DateTime? fromDate, DateTime? toDate)
        {
            IList<ExpensesMdl> items;
            IFilter inventoryMoveDetailFilter = _GetInstance(categoryId, fromDate, toDate);
            items = _repo.GetFilter(inventoryMoveDetailFilter).ToList();
            return items;
        }
        private IFilter _GetInstance(int? categoryId, DateTime? fromDate, DateTime? toDate)
        {
            IFilter filter = DependenciesIOC.GetInstanceUC<IFilter, ExpensesBetweenDatesFilter>();
            Dictionary<string, object> dctnry = new Dictionary<string, object>();
            if (categoryId != null)
                dctnry.Add(PropertyHelper.GetPropertyName((ExpensesMdl v) => v.categoryId), Convert.ToInt32(categoryId));
            if (fromDate != null)
            {
                dctnry.Add(PropertyHelper.GetPropertyName((ExpensesMdl v) => v.theDate), ((DateTime)fromDate).StartOfDay());
                dctnry.Add("toDate", ((DateTime)toDate).EndOfDay());
            }
            filter.SetFilter(dctnry);
            return filter;
        }
        

    }
}
