using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.DefaultValues
{
    public class ExpenseCategoryValues
    {
        public List<ExpenseCategoryMdl> SetValues()
        {
            return new List<ExpenseCategoryMdl>()
            {
                new ExpenseCategoryMdl()
                {
                    id=1,
                    name="عام",
                    isLimitAmount=false,
                    limitAmount=0,
                    created_by= ConstantsShared.DefaultUserId,
                    created_in = MyDateTime.GetDateTime()
                },

                new ExpenseCategoryMdl()
                {
                    id=2,
                    name="التواصل والنت",
                    isLimitAmount=false,
                    limitAmount=0,
                    created_by= ConstantsShared.DefaultUserId,
                    created_in = MyDateTime.GetDateTime()
                },


            };

        }
    }
}
