using ExpenseTracker.Core.DTOs.General;
using System;


namespace ExpenseTracker.Core.FixedCollections.ExpenseCategory
{
    public class ExpenseType : ValueId8Dto
    {
        // نوع المصروف : اساسي - ثانوي
        internal static List<ExpenseType> _GetAll()
        {
            return new List<ExpenseType>()
            {
                new ExpenseType(){id= 1,name="اساسي"},
                new ExpenseType(){id= 2,name="ثانوي"},
            };
        }

        public static List<ExpenseType> GetAll()
        {
            return _GetAll();
        }
    }
}
