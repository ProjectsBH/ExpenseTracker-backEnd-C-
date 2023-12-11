using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Helper.ControlHelper
{
    public static class DataGridViewColumnHelper
    {
        /*
         * كلاس التعديل على اعمدة اداءة العرض من ناحية الاخفاء مثلا
         * فيها دوالتي إضافة اعمدة الى إداءة العرض
         * DataGridView
        */

        public static void ColumnHeaderText(this DataGridView dgv, params KeyValuePair<string, string>[] parm)
        {
            //dGV.Columns["id"].HeaderText = "المعرف";
            for (int i = 0; i < parm.Length; i++)
            {
                dgv.Columns[parm[i].Key].HeaderText = parm[i].Value;
            }
        }

        public static void ColumnHide(this DataGridView dgv, params string[] parm)
        {
            for (int i = 0; i < parm.Length; i++)
            {
                dgv.Columns[parm[i]].Visible = false;
            }
        }
        public static void ColumnHide(params DataGridViewColumn[] parm)
        {
            for (int i = 0; i < parm.Length; i++)
            {
                //dGV.Columns["id"].Visible = false;
                parm[i].Visible = false;
            }
        }
        public static void ColumnHideAll(this DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = false;
            }
        }
        public static void ColumnMiddleLeft(this DataGridView dgv, params string[] parm)
        {
            for (int i = 0; i < parm.Length; i++)
            {
                //dGV.Columns["fullName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[parm[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        public static void AddColumnsForDGV(this DataGridView dGV, string name, string headerText, string txt)
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = name;
                button.HeaderText = headerText;
                button.Text = txt;
                //button.ToolTipText = "حذف السجل";
                button.UseColumnTextForButtonValue = true;
                dGV.Columns.Add(button);
            }
        }
        public static void InsertColumnsForDGV(this DataGridView dGV, string name, string headerText, string txt, int columnIndex = 0)
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = name;
                button.HeaderText = headerText;
                button.Text = txt;
                //button.ToolTipText = "حذف السجل";
                button.UseColumnTextForButtonValue = true;
                dGV.Columns.Insert(columnIndex, button);
            }
        }

    }
}
