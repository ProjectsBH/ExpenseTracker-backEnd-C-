using Dapper;
using ExpenseTracker.DataMySQL.DRY;
using ExpenseTracker.DataMySQL.Extensions;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Filter;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataMySQL.Repository
{
    public class ExpensesRepoImpl : IExpensesRepo
    {
        internal const string tableName = SchemaDB.GetSchemaName + "expensesTb";
        private readonly IDbStrategyUOW _dbStrategyUOW;
        private readonly IDbConnection _connection;
        private Type type = typeof(ExpensesRepoImpl);
        public ExpensesRepoImpl(IDbStrategyUOW dbStrategyUOW)
        {
            _dbStrategyUOW = dbStrategyUOW;
            _connection = _dbStrategyUOW.Connection;
        }
        

        public IEnumerable<ExpensesMdl> GetTop(int topNo = 50)
        {
            try
            {
                return _connection.GetTop<ExpensesMdl>(tableName
                    , topNo);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetTop method error :" + type, ex);
            }
        }

        public IEnumerable<ExpensesMdl> CheckCategoryIdHasExpenses(int categoryId)
        {
            try 
            {
                var items = _connection.GetTopBy<ExpensesMdl>(tableName
                    , "categoryId = @id"
                    , new { id = categoryId }, 1);
                return items;
                //return items.FirstOrDefault()!;

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetTop method error :" + type, ex);
            }
        }

        public ExpensesMdl GetById(object id)
        {
            try
            {
                return _connection.GetSingleBy<ExpensesMdl>(tableName
                    , "id = @id"
                    , new { id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetById method error :" + type, ex);
            }
        }

        public IEnumerable<ExpensesMdl> GetFilter(IFilter filter)
        {
            try
            {
                return _connection.GetAllBy<ExpensesMdl>(tableName
                    , filter.GetWhere()
                    , filter.GetFilter());
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetByFilter method error :" + type, ex);
            }
        }
        public Tuple<bool, object> Add(ExpensesMdl entity)
        {
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@categoryId", entity.categoryId);
                dp.Add("@theDate", entity.theDate);
                dp.Add("@amount", entity.amount);
                dp.Add("@theStatement", entity.theStatement);
                dp.Add("@created_by", entity.created_by);
                dp.Add("@created_in", entity.created_in);

                long result = _connection.InsertScalar<long>(tableName
                    , "categoryId, theDate, amount, theStatement, created_by, created_in"
                    , "@categoryId, @theDate, @amount, @theStatement, @created_by, @created_in"
                    , dp);
                return Tuple.Create(result > 0, (object)result);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }

        public bool Delete(long entity_id)
        {
            try
            {
                int result = _connection.Delete(tableName, "id = @id", new { id = entity_id });
                return (result > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Delete method error :" + type, ex);
            }
        }
        public bool Update(ExpensesMdl entity)
        {
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@id", entity.id);
                dp.Add("@categoryId", entity.categoryId);
                dp.Add("@theDate", entity.theDate);
                dp.Add("@amount", entity.amount);
                dp.Add("@theStatement", entity.theStatement);

                int result = _connection.Update(tableName
                    , "categoryId=@categoryId, theDate=@theDate, amount=@amount, theStatement=@theStatement"
                    , "id = @id"
                    , dp);
                return (result > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Update method error :" + type, ex);
            }
        }


    }
}
