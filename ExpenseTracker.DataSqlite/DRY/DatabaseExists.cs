using Dapper;
using ExpenseTracker.DataSqlite.Extensions;
using ExpenseTracker.DataSqlite.Repository;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Shared.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataSqlite.DRY
{
    public class DatabaseExists
    {
        private readonly IDbConnection dal;
        public DatabaseExists(IDbConnection _connection)
        {
            dal = _connection;
        }
        private int ExecuteCommand(string query)
        {
            int result = 0;
            result= dal.ExecuteQuery(query);
            return result;
            //using (SQLiteConnection Connection = new SQLiteConnection(AppSettings.DataBaseSettings.ConnectionString))
            //{
            //    SQLiteCommand cmd = new SQLiteCommand(query, Connection);

            //    Connection.Open();
            //    result = cmd.ExecuteNonQuery();
            //    Connection.Close();
            //}
            //return result;
        }
        public void CheckIfDatabaseExists()
        {
            try
            {
                if (!File.Exists(AppSetting.DBName))
                {
                    //SQLiteConnection.CreateFile(AppSetting.DBName);
                    using (FileStream fsStrean = File.Create(AppSetting.DBName))
                    {
                        fsStrean.Close();
                        fsStrean.Dispose();
                    }

                    // userTb //id, userName ,password,email,created_in
                    string commandString = "CREATE TABLE IF NOT EXISTS " + UserRepoImpl.tableName + " (id INTEGER PRIMARY KEY AUTOINCREMENT,userName NVARCHAR(50) NOT NULL,password NVARCHAR(150), email NVARCHAR(250), created_in NVARCHAR(50))";
                    ExecuteCommand(commandString);

                    // expenseCategoryTb
                    commandString = "Create Table IF NOT EXISTS " + ExpenseCategoryRepoImpl.tableName + " (id INTEGER, name NVARCHAR(50) NOT NULL, isLimitAmount int DEFAULT 0, limitAmount NUMERIC DEFAULT 0, created_in NVARCHAR(50), created_by int,PRIMARY KEY(id)" +
                        " , CONSTRAINT fk_UserAndExpenseCategory FOREIGN KEY(created_by) REFERENCES " + UserRepoImpl.tableName + "(id))";
                    ExecuteCommand(commandString);
                    //dd = dal.ExecuteQuery(commandString);

                    // expensesTb
                    commandString = "Create Table IF NOT EXISTS " + ExpensesRepoImpl.tableName + " (id INTEGER, categoryId int NOT NULL" +
                        ", theDate NVARCHAR(50) NOT NULL, amount NUMERIC NOT NULL" +
                        ", theStatement TEXT, created_in NVARCHAR(50), created_by int " +
                        ", PRIMARY KEY(id), CONSTRAINT fk_categoryAndExpenses FOREIGN KEY(categoryId) REFERENCES " + ExpenseCategoryRepoImpl.tableName + "(id)" +
                        ", CONSTRAINT fk_UserAndExpenses FOREIGN KEY(created_by) REFERENCES " + UserRepoImpl.tableName + "(id))";
                    ExecuteCommand(commandString);
                    
                    // add the default
                    commandString = "Insert into " + UserRepoImpl.tableName + " (userName ,password, email, created_in) values ('basheer','123456','basheer@gmail.com','" + DateTime.Now.ToString() + "' )";
                    ExecuteCommand(commandString);
                   

                }
            }
            catch (Exception ex)
            {
                if (File.Exists(AppSetting.DBName))
                    File.Delete(AppSetting.DBName);
                throw new Exception("DatabaseExists method error :", ex);
                //throw;
            }


        }

    }
}
