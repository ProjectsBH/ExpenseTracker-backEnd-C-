using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Shared
{
    public class ConstantsShared
    {
        public static int DefaultUserId = SessionData.idUserMy;// رقم المستخدم الافتراضي عند اضافة سجلات التهيئة من قبل المبرمجين
        public const string bh = "j";
    }
}
