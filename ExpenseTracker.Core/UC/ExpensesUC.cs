using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.Utils;
using ExpenseTracker.Core.Utils.FinalResults;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ExpenseTracker.Core.UC
{
    public class ExpensesUC : IExpenses
    {
        private readonly IExpensesRepo _repo;
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        public ExpensesUC(IExpensesRepo repo, IExpenseCategoryRepo expenseCategoryRepo)
        {
            _repo = repo;
            _expenseCategoryRepo= expenseCategoryRepo;
        }
        public string title => "المصروفات";

        private Tuple<bool, string> Valid(ExpenseRequestDto entity)
        {
            var result = ValidationModels.Validated(entity);
            if (result.Item1 == false)
                return Tuple.Create(false,result.Item2);

            if (entity.amount < 1)
                return Tuple.Create(false,"مبلغ العملية غير مقبول");

            var itemCategory = _expenseCategoryRepo.GetById(entity.categoryId);
            if (itemCategory.isLimitAmount && (entity.amount > itemCategory.limitAmount))
                return Tuple.Create(false, "مبلغ العملية أكبر من حد السقف");
            
            return Tuple.Create(true, "");
        }
        public ServicesResultsDto Add(ExpenseRequestDto entity)
        {
            try
            {
                // Valid
                var tuple = Valid(entity);
                if (tuple.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(tuple.Item2);

                ExpensesMdl mdl = entity.toModel(entity);

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

        

        public ServicesResultsDto Edit(long id, ExpenseRequestDto entity)
        {
            try
            {
                // Valid
                var tuple = Valid(entity);
                if (tuple.Item1 == false)
                    return ServicesResultsDRY.GetIncorrectInput(tuple.Item2);

                ExpensesMdl mdl = entity.toModel(id, entity);

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
        public ServicesResultsDto Delete(long id)
        {
            try
            {
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
        public IList<ExpenseResponseDto> GetTop(int topNo)
        {
            try
            {
                var items = _repo.GetTop(topNo).ToList();
                IList<ExpenseResponseDto> entities = new ExpenseResponseDto().fromModel(items);
                return entities;
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }
        public ExpenseResponseDto GetBy(long id)
        {
            try
            {
                var item = _repo.GetById(id);
                ExpenseResponseDto entity = new ExpenseResponseDto().fromModelSearch(item);
                return entity;
            }
            catch (Exception)
            {
                ServicesResultsDRY.GetException();
                throw;
            }
        }


    }
}
