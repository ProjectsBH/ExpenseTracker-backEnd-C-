using ExpenseTracker.Core.Filter.Expenses;
using ExpenseTracker.Domain.Filter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Configuration
{
    public static partial class DependenciesIOC
    {
        private static IServiceCollection Filter(this IServiceCollection services)
        {
            // Filter : Expenses
            services.AddTransient<IFilter, ExpensesCategoryFilter>();
            services.AddTransient<IFilter, ExpensesTheDateFilter>();
            services.AddTransient<IFilter, ExpensesBetweenDatesFilter>();
          

            // Filter : Act
            // Filter : Act
            // Filter : Act


            return services;
        }

    }
}
