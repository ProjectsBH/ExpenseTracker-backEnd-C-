using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.UC;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ExpenseTracker.Core.Configuration
{
    public static partial class DependenciesIOC
    {
        private static ServiceProvider ServiceProvider = null;
        public static T GetInstanceUC<T>()//GetInstanceUC
        {
            return ServiceProvider.GetRequiredService<T>();
        }
        public static T GetInstanceUC<T, TM>()
        {
            var services = ServiceProvider.GetServices<T>();
            var serviceTM = services.First(o => o.GetType() == typeof(TM));
            return serviceTM;
        }
        public static IServiceCollection ConfigureUseCase(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserUC>();
            services.AddSingleton<IExpenseCategory, ExpenseCategoryUC>();
            services.AddScoped<IExpenses, ExpensesUC>();

            services.AddScoped<IExpensesReport, ExpensesReportUC>();


            services.Filter();
            services.ConfigureData();


            ServiceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}
