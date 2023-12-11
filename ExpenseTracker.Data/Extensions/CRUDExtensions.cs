using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Extensions
{
    // install Dapper
    internal static class CRUDExtensions
    {

        public static int Insert(this IDbConnection cnn, string tableName, string tableColumns, string values, object param, IDbTransaction transaction = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(tableName);
            query.Append(" (");
            query.Append(tableColumns);
            query.Append(") VALUES (");
            query.Append(values);
            query.Append(")");
            return cnn.Execute(query.ToString(), param, transaction: transaction);
        }
        public static T InsertScalar<T>(this IDbConnection cnn, string tableName, string tableColumns, string values, object param, IDbTransaction transaction = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(tableName);
            query.Append(" (");
            query.Append(tableColumns);
            query.Append(") VALUES (");
            query.Append(values);
            query.Append("); Select Cast (Scope_Identity() as int)");
            return cnn.ExecuteScalar<T>(query.ToString(), param, transaction: transaction);
        }
        public static int Update(this IDbConnection cnn, string tableName, string tableColumnsWithValues, string where, object param, IDbTransaction transaction = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Update ");
            query.Append(tableName);
            query.Append(" set ");
            query.Append(tableColumnsWithValues);
            query.Append(" WHERE ");
            query.Append(where);
            //var query = "Update " + tableName + " set "+ tableColumns +"  WHERE "+ where;
            return cnn.Execute(query.ToString(), param, transaction: transaction);
        }
        public static int Delete(this IDbConnection cnn, string tableName, string where, object param, IDbTransaction transaction = null)
        {
            var query = "Delete FROM " + tableName + "  WHERE " + where;
            return cnn.Execute(query, param, transaction: transaction);
        }
        public static int DeleteAll(this IDbConnection cnn, string tableName, IDbTransaction transaction = null)
        {
            var query = "Delete FROM " + tableName;
            return cnn.Execute(query, null, transaction: transaction);
        }

        public static IEnumerable<T> GetAll<T>(this IDbConnection cnn, string tableName, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //var query = "SELECT "+ tableColumns+" FROM " + tableName;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            return cnn.Query<T>(query.ToString(), transaction: transaction, commandType: CommandType.Text);
        }
        public static IEnumerable<T> GetAllQuery<T>(this IDbConnection cnn, string query, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return cnn.Query<T>(query, transaction: transaction, commandType: CommandType.Text);
        }
        // Difference between IEnumerable and IQueryable : https://josipmisko.com/posts/c-sharp-iqueryable-vs-ienumerable
        public static IQueryable<T> GetAllQuery<T>(this IDbConnection cnn, string tableName, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //var query = "SELECT "+ tableColumns+" FROM " + tableName;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            return cnn.Query<T>(query.ToString(), transaction: transaction, commandType: CommandType.Text).AsQueryable();
        }
        public static IEnumerable<T> GetAllBy<T>(this IDbConnection cnn, string tableName, string where, object param, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            query.Append(" WHERE ");
            query.Append(where);
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text);
        }
        public static IEnumerable<T> GetAllQueryBy<T>(this IDbConnection cnn, string queryText, string where, object param, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            // Query from more than one table - استعلام من أكثر من جدول
            StringBuilder query = new StringBuilder();
            query.Append(queryText);
            query.Append(" WHERE ");
            query.Append(where);//order by theDate [asc - desc] // order by dateTrans,id
            query.Append("  ");
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text);

        }
        public static T GetSingleQueryBy<T>(this IDbConnection cnn, string queryText, string where, object param, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            StringBuilder query = new StringBuilder();
            query.Append(queryText);
            query.Append(" WHERE ");
            query.Append(where);
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text).SingleOrDefault();
        }
        public static T GetSingleBy<T>(this IDbConnection cnn, string tableName, string where, object param, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            // SELECT TOP 1 [Id] FROM[MyTable] ORDER BY[Id] ASC // in (SQL Server)
            // SELECT [Id] FROM [MyTable] ORDER BY [Id] ASC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY // in (SQL Server)
            //return cn.Query<ManagerTitleMdl>("SELECT * FROM " + tableName + " WHERE id=@ID", new { ID = id }).SingleOrDefault();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1 ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            query.Append(" WHERE ");
            query.Append(where);
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text).SingleOrDefault();
        }
        public static T GetFirstBy<T>(this IDbConnection cnn, string tableName, string where, object param, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //return cn.QueryFirst<ManagerTitle>("SELECT * FROM "+ tableName+" WHERE id=@id", new { id = id });
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            query.Append(" WHERE ");
            query.Append(where);
            return cnn.QueryFirst<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text);
        }
        public static T GetMax<T>(this IDbConnection cnn, string tableName, string nameColumn = "id", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //return cn.Query<MaxId>("select MAX(id) AS id from "+tableName);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT MAX(");
            query.Append(nameColumn);
            query.Append(") AS id FROM ");
            query.Append(tableName);
            return cnn.Query<T>(query.ToString(), transaction: transaction, commandType: CommandType.Text).SingleOrDefault();
        }

        public static T GetMaxBy<T>(this IDbConnection cnn, string tableName, string where, object param, string nameColumn = "id", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT MAX(");
            query.Append(nameColumn);
            query.Append(") AS id FROM ");
            query.Append(tableName);
            query.Append(" WHERE ");
            query.Append(where);
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text).SingleOrDefault();
        }
        public static IEnumerable<T> GetTop<T>(this IDbConnection cnn, string tableName, int topNo = 50, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //var query = "SELECT Top 50 "+ tableColumns+" FROM " + tableName;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT Top ");
            query.Append(topNo);
            query.Append(" ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            query.Append(" order by created_in desc ");
            return cnn.Query<T>(query.ToString(), transaction: transaction, commandType: CommandType.Text);

        }
        public static IEnumerable<T> GetTopBy<T>(this IDbConnection cnn, string tableName, string where, object param, int topNo = 50, string tableColumns = "*", IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            //var query = "SELECT Top 50 "+ tableColumns+" FROM " + tableName;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT Top ");
            query.Append(topNo);
            query.Append(" ");
            query.Append(tableColumns);
            query.Append(" FROM ");
            query.Append(tableName);
            query.Append(" WHERE ");
            query.Append(where);
            query.Append(" order by created_in desc ");
            return cnn.Query<T>(query.ToString(), param, transaction: transaction, commandType: CommandType.Text);
        }

    }
}
