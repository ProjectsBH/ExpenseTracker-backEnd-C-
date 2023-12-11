using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.Utils.FinalResults;
using ExpenseTracker.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Domain.IRepository;
using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Core.Configuration;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Core.Filter.Expenses;

namespace ExpenseTracker.Core.UC
{
    public class ExpenseCategoryUC : IExpenseCategory
    {
        private readonly IExpenseCategoryRepo _repo;
        private IList<ExpenseCategoryResponseDto> lstData = null;
        public ExpenseCategoryUC(IExpenseCategoryRepo repo)
        {
            _repo = repo;
        }
        public string title => "فئات المصروفات";

        public ServicesResultsDto Add(ExpenseCategoryRequestDto entity)
        {
            try
            {
                // Valid
                var result = ValidationModels.Validated(entity);
                if (result.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(result.Item2);

                if (entity.isLimitAmount == true && entity.limitAmount<1)
                    return ServicesResultsDRY.GetIncorrectInput("مبلغ حد السقف غير مقبول");
                
                var tuple = ValidForExists(entity.name);
                if (tuple.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(tuple.Item2);

                ExpenseCategoryMdl mdl = entity.toModel(entity);

                var tupleAdd = _repo.Add(mdl);
                var isDone = tupleAdd.Item1;
                if (isDone)
                    return ServicesResultsDRY.GetSuccessWithId(tupleAdd.Item2);
                else return ServicesResultsDRY.GetError(ResultsTypes.None);
            }
            catch (Exception)
            {
                return ServicesResultsDRY.GetException();
                throw;
            }
        }
        private Tuple<bool, string> ValidForExists(string nameAr, int? id = null)
        {
            var item = _repo.GetForExists(nameAr, id);
            if (item != null && item.id > 0)
                return Tuple.Create(false, "البيانات الحالية مكررة");
            return Tuple.Create(true, "");
        }
        public ServicesResultsDto Edit(int id, ExpenseCategoryRequestDto entity)
        {
            try
            {
                // Valid
                var result = ValidationModels.Validated(entity);
                if (result.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(result.Item2);

                if (entity.isLimitAmount == true && entity.limitAmount < 1)
                    return ServicesResultsDRY.GetIncorrectInput("مبلغ حد السقف غير مقبول");

                var tuple = ValidForExists(entity.name, id);
                if (tuple.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(tuple.Item2);

                ExpenseCategoryMdl mdl = entity.toModel(id, entity);

                var isDone = _repo.Update(mdl);
                if (isDone)
                    return ServicesResultsDRY.GetSuccess();
                else return ServicesResultsDRY.GetError(ResultsTypes.None);
            }
            catch (Exception)
            {
                return ServicesResultsDRY.GetException();
                throw;
            }
        }

        public ServicesResultsDto Delete(int id)
        {
            try
            {
                
                IExpensesRepo _expensesRepo = DependenciesIOC.GetInstanceUC<IExpensesRepo>();
                var items = _expensesRepo.CheckCategoryIdHasExpenses(id);
                if(items != null && items.Count()>0)
                    return ServicesResultsDRY.GetIncorrectInput("الفئة الحالية لها مصروفات لا يمكن حذفه");

                var isDone = _repo.Delete(id);
                if (isDone)
                    return ServicesResultsDRY.GetSuccess();
                else return ServicesResultsDRY.GetError(ResultsTypes.None);
            }
            catch (Exception)
            {
                return ServicesResultsDRY.GetException();
                throw;
            }
        }

        public IList<ExpenseCategoryResponseDto> GetAll()
        {
            try
            {
                return _GetAll(true);
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }
        private IList<ExpenseCategoryResponseDto> _GetAll(bool refresh = false)
        {
            if (lstData == null || refresh)
            {
                var items = _repo.GetAll().ToList();
                IList<ExpenseCategoryResponseDto> entities = new ExpenseCategoryResponseDto().fromModel(items);
                lstData = entities;
            }
            return lstData;
            
        }
        public IList<ExpenseCategoryResponseDto> GetByName(string value)
        {
            try
            {
                if (lstData == null)
                    _GetAll();
                var results = lstData.Where(p => p.name.ToLower().Contains(value.ToLower())).ToList();
                return results;
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }

        
    }
}
