using Dapper;
using ExpenseTracker.DataSqlite.DRY;
using ExpenseTracker.DataSqlite.DRY.Models;
using ExpenseTracker.DataSqlite.Extensions;
using ExpenseTracker.Domain.DefaultValues;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataSqlite.Repository
{
    public class ExpenseCategoryRepoImpl : IExpenseCategoryRepo
    {
        internal const string tableName = SchemaDB.GetSchemaName + "expenseCategoryTb";
        private readonly IDbStrategyUOW _dbStrategyUOW;
        private readonly IDbConnection _connection;
        private Type type = typeof(ExpenseCategoryRepoImpl);
        public ExpenseCategoryRepoImpl(IDbStrategyUOW dbStrategyUOW)
        {
            _dbStrategyUOW = dbStrategyUOW;
            _connection = _dbStrategyUOW.Connection;
        }

        public IEnumerable<ExpenseCategoryMdl> GetAll()
        {
            try
            {
                var items = _connection.GetAll<ExpenseCategoryMdl>(tableName);
                if (items.Any() == false)
                {
                    DefaultValuesAdd();
                    items = new ExpenseCategoryValues().SetValues();
                }
                return _connection.GetAll<ExpenseCategoryMdl>(tableName);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} All method error :" + type, ex);
            }
        }

        public ExpenseCategoryMdl GetById(object id)
        {
            try
            {
                return _connection.GetSingleBy<ExpenseCategoryMdl>(tableName
                    , "id = @id"
                    , new { id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetById method error :" + type, ex);
            }
        }
        public ExpenseCategoryMdl GetForExists(string name, object? id)
        {
            try
            {
                string where = "name=@name";
                return _connection.GetSingleBy<ExpenseCategoryMdl>(tableName
                    , id == null ? where : where + " and id != @id"
                    , new { name = name, id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetForExists method error :" + type, ex);
            }
        }
        public Tuple<bool, object> Add(ExpenseCategoryMdl entity)
        {
            try
            {
                var maxId = _connection.GetMax<MaxId>(tableName);
                int id = maxId.id;
                if (id < 1)
                {
                    DefaultValuesAdd();
                    id += 1;
                }
                else
                    id += 1;
                entity.id = id;
                return _Add(entity);

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }
        private Tuple<bool, object> _Add(ExpenseCategoryMdl entity)
        {
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@id", entity.id);
                dp.Add("@name", entity.name);
                dp.Add("@isLimitAmount", entity.isLimitAmount);
                dp.Add("@limitAmount", entity.limitAmount);
                dp.Add("@created_by", entity.created_by);
                dp.Add("@created_in", entity.created_in);

                int result = _connection.Insert(tableName
                    , "id, name, isLimitAmount, limitAmount, created_in, created_by"
                    , "@id, @name, @isLimitAmount, @limitAmount, @created_in, @created_by"
                    , dp);
                return Tuple.Create(result > 0, (object)entity.id);

            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }
        public bool Update(ExpenseCategoryMdl entity)
        {
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@id", entity.id);
                dp.Add("@name", entity.name);
                dp.Add("@isLimitAmount", entity.isLimitAmount);
                dp.Add("@limitAmount", entity.limitAmount);

                int result = _connection.Update(tableName
                    , "name=@name, isLimitAmount=@isLimitAmount, limitAmount=@limitAmount"
                    , "id = @id"
                    , dp);
                return (result > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Update method error :" + type, ex);
            }
        }
        public bool Delete(int entity_id)
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

        private void DefaultValuesAdd()
        {
            var lst = new ExpenseCategoryValues().SetValues();
            foreach (var item in lst)
            {
                var tuple = _Add(item);
            }
        }



    }
}
