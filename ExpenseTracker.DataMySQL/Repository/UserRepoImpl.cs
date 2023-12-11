using Dapper;
using ExpenseTracker.DataMySQL.DRY;
using ExpenseTracker.DataMySQL.Extensions;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.DataMySQL.Repository
{
    public class UserRepoImpl : IUserRepo
    {
        internal const string tableName = SchemaDB.GetSchemaName + "userTb";
        private readonly IDbStrategyUOW _dbStrategyUOW;
        private readonly IDbConnection _connection;
        private Type type = typeof(UserRepoImpl);
        public UserRepoImpl(IDbStrategyUOW dbStrategyUOW)
        {
            _dbStrategyUOW = dbStrategyUOW;
            _connection = _dbStrategyUOW.Connection;
        }
        public Tuple<bool, UserMdl?> Login(string userName, string password)
        {
            try
            {
                UserMdl item = _connection.GetSingleBy<UserMdl>(tableName
                    , "userName = @userName and password = @password"
                    , new { userName = userName, password = password });
                return Tuple.Create((item != null && item.id > 0) , item ?? (new UserMdl()));
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetForExists method error :" + type, ex);
            }
        }

        public Tuple<bool, object> SignUp(UserMdl entity)
        {
            try
            {
                var dp = new DynamicParameters();
                dp.Add("@userName", entity.userName);
                dp.Add("@password", entity.password);
                dp.Add("@email", entity.email);
                dp.Add("@created_in", entity.created_in);

                int result = _connection.InsertScalar<int>(tableName
                    , "userName, password, email, created_in"
                    , "@userName, @password, @email, @created_in"
                    , dp);
                return Tuple.Create(result > 0, (object)result);
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} Add method error :" + type, ex);
            }
        }

        public IEnumerable<UserMdl> GetDuplicate(string userName, string password)
        {
            try
            {
                var items = _connection.GetAllBy<UserMdl>(tableName
                    , "userName = @userName or (userName = @_userName and password = @password)"
                    , new { userName = userName, _userName = userName, password = password });
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("{Repo} GetDuplicate method error :" + type, ex);
            }
        }


    }
}
