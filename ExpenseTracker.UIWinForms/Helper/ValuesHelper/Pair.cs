using System.Collections.Generic;

namespace ExpenseTracker.UIWinForms.Helper.ValuesHelper
{
    public class Pair
    {
        //dGV.ColumnHeaderText(new KeyValuePair<string, string>("fullName", "اسم المسافر")
        //    , new KeyValuePair<string, string>("mobileNumber", "رقم الموبايل"));
        public static KeyValuePair<string, string> Of(string key, string value)
        {
            // لكي يرجع ابجيكت من هذا النوع للإسناده الى دالة برامترها من نفس النوع بدلا من كلمة
            // new KeyValuePair<string, string>
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
