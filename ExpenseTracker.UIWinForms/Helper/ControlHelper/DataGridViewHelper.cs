using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Helper.ControlHelper
{
    public static class DataGridViewHelper
    {
        /*
         * كلاس لكي ناخذ البيانات من اداة العرض
         * DataGridView
         * وإسنادها للكلاس محدد
        */

        // GetIdFromDataGridView // إرجاع رقم الصف المحدد من أداة العرض وهذ الرقم يكون وحيد ولا يكرر
        public static int GetIdFromDGV(this DataGridView dgv, string columnName = "id")
        {
            //int id = Convert.ToInt32(dgv.CurrentRow.Cells["id"].Value.ToString()); // Edit
            //int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value); // Delete

            int id = Convert.ToInt32(dgv.CurrentRow.Cells[columnName].Value.ToString());
            return id;
        }
        public static long GetId64FromDGV(this DataGridView dgv, string columnName = "id")
        {
            long id = Convert.ToInt64(dgv.CurrentRow.Cells[columnName].Value.ToString());
            return id;
        }
        // ارجاع بيانات ابجيكت لكلاس محدد من لست من الكلاس نفسه
        // ارجاع بيانات كلاس محدد من لست من الكلاس نفسه
        public static T GetEntity<T>(this IList<T> t, Func<T, bool> predicate)
        {
            return t.Where(predicate).FirstOrDefault();
        }
    }
}
