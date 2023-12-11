using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.UIWinForms
{
    public static class GeneralVariables
    {
        // هل تم اضافة/ تعديل/ حذف في واجهة ما لكي يتم التحديث
        public static bool isExecute { get; set; } = false;
        public static string AppName = "مصروفاتي - Expense Tracker";
        public static void setAppName(Label lbl)
        {
            lbl.Text = AppName;
        }
    }
}
