
using ExpenseTracker.Core.Configuration;
using ExpenseTracker.Shared.Infrastructure.Settings;
using ExpenseTracker.UIWinForms.Views;
using ExpenseTracker.UIWinForms.Views.Sub;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Config
{
    public static class IoC
    {
        // بسبب تكرار هذا هنا يكرر ابجيكت SQLiteStrategyUOW ومن شابهها
        // private static ServiceProvider ServiceProvider = null;

        public static T GetForm<T>() where T : Form //IoC.GetForm<>()
        {
            return DependenciesIOC.GetInstanceUC<T>();
        }
        // بسبب تكرار هذا هنا يكرر ابجيكت SQLiteStrategyUOW ومن شابهها
        //public static T GetInstanceUC<T>() //IoC.GetInstanceUC<>()
        //{
        //    return ServiceProvider.GetRequiredService<T>();
        //}
        public static void Init()
        {
            var services = new ServiceCollection();
            
            AppSettings.DBProvider = DataBaseProvider.SQLite;
            string ConnectionStringSQLServer = ConfigurationManager.AppSettings["DBConnection"].ToString();
            string ConnectionStringMySQL = "Server=localhost;Database=expensetrackerdb_api;Uid=root;Pwd=;";
            string ConnectionStringSqlLite = "Data Source=expensetrackerdb_api; Version=3";
            string connectionType()
            {
                string connStr = ConnectionStringSqlLite;
                switch (AppSettings.DBProvider)
                {
                    case DataBaseProvider.SqlServer:
                        connStr = ConnectionStringSQLServer;
                        break;
                    case DataBaseProvider.MySQL: connStr = ConnectionStringMySQL; break;
                    default: connStr = ConnectionStringSqlLite; break;
                }
                return connStr;
            }

            AppSettings.DataBaseSettings = new DataBaseSettings
            {
                //ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString,
                //ConnectionString = ConfigurationManager.AppSettings["DBConnection"],
                //ConnectionString = AppSettings.DBProvider == DataBaseProvider.SqlServer ? ConnectionStringSQLServer : ConnectionStringMySQL,
                ConnectionString = connectionType(),
                DBName = ConfigurationManager.AppSettings["DataBaseName"],
            };

            services.UI();
            services.ConfigureUseCase();

            // بسبب تكرار هذا هنا يكرر ابجيكت SQLiteStrategyUOW ومن شابهها
            //ServiceProvider = services.BuildServiceProvider(); 
        }
        private static void UI(this IServiceCollection services)
        {
            RegisterOthersClasses(services);

            RegisterFormsGeneral(services);

            
        }
        private static void RegisterOthersClasses(IServiceCollection services)
        {
            services.AddTransient<SaveFileDialog>();
        }

       

        #region
        private static void RegisterFormsGeneral(IServiceCollection services)
        {
            services.AddTransient<Form_Login>();
            services.AddTransient<Form_splash>();
            services.AddTransient<MainForm>();
            services.AddTransient<ExpenseCategoryForm>();
            services.AddTransient<ExpenseForm>();
            services.AddTransient<AddExpenseCategoryForm>();
            services.AddTransient<ExpensesReportForm>();
            services.AddTransient<ExpensesSearchForm>();
        }




        #endregion

        #region

        #endregion

        #region

        #endregion

    }
}
