using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Helper.ControlHelper
{
    public static class ComboBoxHelper
    {
        public static void SetCmbData<T>(ComboBox cmb, Func<IList<T>> ac, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            SetData(cmb, ac, isSelectedFirst, displayMember, valueMember);
        }
        public static void SetData<T>(ComboBox cmb, Func<IList<T>> ac, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = ac();//ac.Invoke();
            cmb.DataSource = method;//method.AsEnumerable().ToList();
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }
        public static void SetCmbDataBy<T>(ComboBox cmb, Func<int, IList<T>> ac, int funcId, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            SetDataBy(cmb, ac, funcId, isSelectedFirst, displayMember, valueMember);
        }
        public static void SetDataBy<T>(ComboBox cmb, Func<int, IList<T>> ac, int funcId, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = ac.Invoke(funcId);//ac(funcId)
            cmb.DataSource = method;
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }
        public static void SetDataBy<T>(ComboBox cmb, Func<byte, IList<T>> ac, byte funcId, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = ac(funcId);
            cmb.DataSource = method;
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }
        public static void SetCmbData<T>(ComboBox cmb, Func<bool, IList<T>> ac, bool funcId, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            SetDataBy(cmb, ac, funcId, isSelectedFirst, displayMember, valueMember);
        }
        public static void SetDataBy<T>(ComboBox cmb, Func<bool, IList<T>> ac, bool funcId, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = ac(funcId);
            cmb.DataSource = method;
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }
        public static void SetData<T>(ComboBox cmb, Func<List<T>> ac, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = ac();//ac.Invoke();
            cmb.DataSource = method;
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }

        public static void SetCmbData<T>(ComboBox cmb, List<T> lst, bool isSelectedFirst = false, string displayMember = "name", string valueMember = "id")
        {
            var method = lst;
            cmb.DataSource = method;
            cmb.DisplayMember = displayMember;
            cmb.ValueMember = valueMember;
            if (isSelectedFirst && method != null && method.Count > 0)
                cmb.SelectedIndex = 0;
            else cmb.SelectedIndex = -1;
        }

    }
}
