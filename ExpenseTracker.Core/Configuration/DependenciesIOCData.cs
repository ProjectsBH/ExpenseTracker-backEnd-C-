using ExpenseTracker.Data.DAL;
using ExpenseTracker.Data.Repository;
using ExpenseTracker.DataMySQL.DAL;
using System.Data.SQLite;
using ExpenseTracker.DataSqlite.DAL;
using ExpenseTracker.Domain.InterfaceUnitOfWork;
using ExpenseTracker.Domain.IRepository;
using ExpenseTracker.Shared.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mys = ExpenseTracker.DataMySQL.Repository;
using sqlite = ExpenseTracker.DataSqlite.Repository;
using ExpenseTracker.DataMySQL.DRY;

namespace ExpenseTracker.Core.Configuration
{
    public static partial class DependenciesIOC
    {
        private static IServiceCollection ConfigureData(this IServiceCollection services)
        {
            /*
             * تستطيع انشاء اكثر من مشروع خاصة بمصدر البيانات ولوجيك النظام والدومين لن يتغير مهما كان نوع مصدر البيانات
             * حاليا معانا مصدرين هما SqlServer و MySQL
             * فالإختلاف بين الاثنان هو نوع كلاس الاتصال SqlConnection و MySqlConnection ثم
             * تعديلات في بعض الاستعلامات بدل
             * top مستخدمة في استعلام SqlServer
             * limit in MySQL
             * وغيرها من الاختلافات البسيطة
             * ضروري تعمل شرط نوع مزود قاعدة البيانات لكي ينشئ ابجيكت واحد لكل واجهة مجردة IRepo
             */
            if (AppSettings.DBProvider == DataBaseProvider.SqlServer)
                services = ConfigureDataSQLServer(services);
            else if (AppSettings.DBProvider == DataBaseProvider.MySQL)
                services = ConfigureDataMySQL(services);
            else if (AppSettings.DBProvider == DataBaseProvider.SQLite)
                services = ConfigureDataSqlite(services);

            return services;
        }
        private static IServiceCollection ConfigureDataSQLServer(this IServiceCollection services)
        {

            #region Inject DAL

            IDbConnection GetConnection()
            {
                /* إذا لم تفتح عند استدعاء
             * دالة _dbStrategyUOW.CreateTransaction();
             * يرجع خطأ ان نص الاتصال بقاعدة البيانات مغلق
            */
                var connection = new SqlConnection(AppSettings.DataBaseSettings.ConnectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection;
            }

            // 1-
            services.AddSingleton<IDbConnection>(GetConnection());
            services.AddSingleton<IDbStrategyUOW, SqlServerStrategyUOW>();

            #endregion

            //Repository
            services.AddSingleton<IUserRepo, UserRepoImpl>();
            services.AddSingleton<IExpenseCategoryRepo, ExpenseCategoryRepoImpl>();
            services.AddSingleton<IExpensesRepo, ExpensesRepoImpl>();

            return services;
        }

        private static IServiceCollection ConfigureDataMySQL(this IServiceCollection services)
        {

            #region Inject DAL

            IDbConnection GetConnection()
            {
                /* إذا لم تفتح عند استدعاء
             * دالة _dbStrategyUOW.CreateTransaction();
             * يرجع خطأ ان نص الاتصال بقاعدة البيانات مغلق
            */
                var connection = new MySqlConnection(AppSettings.DataBaseSettings.ConnectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection;
            }

            // 1-
            services.AddSingleton<IDbConnection>(GetConnection());
            services.AddSingleton<IDbStrategyUOW, MySQLStrategyUOW>();

            #endregion

            //Repository
            services.AddSingleton<IUserRepo, mys.UserRepoImpl>();
            services.AddSingleton<IExpenseCategoryRepo, mys.ExpenseCategoryRepoImpl>();
            services.AddSingleton<IExpensesRepo, mys.ExpensesRepoImpl>();

            return services;
        }

        private static IServiceCollection ConfigureDataSqlite(this IServiceCollection services)
        {

            #region Inject DAL

            IDbConnection GetConnection()
            {
                /* إذا لم تفتح عند استدعاء
             * دالة _dbStrategyUOW.CreateTransaction();
             * يرجع خطأ ان نص الاتصال بقاعدة البيانات مغلق
            */
                var connection = new SQLiteConnection(AppSettings.DataBaseSettings.ConnectionString);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection;
            }

            // 1-
            services.AddSingleton<IDbConnection>(GetConnection());
            services.AddSingleton<IDbStrategyUOW, SQLiteStrategyUOW>();
            //services.AddSingleton<DatabaseExists>();
            #endregion

            //Repository
            services.AddSingleton<IUserRepo, sqlite.UserRepoImpl>();
            services.AddSingleton<IExpenseCategoryRepo, sqlite.ExpenseCategoryRepoImpl>();
            services.AddSingleton<IExpensesRepo, sqlite.ExpensesRepoImpl>();

            return services;
        }

        

    }
}
