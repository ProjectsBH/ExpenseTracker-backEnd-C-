using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Config.Helpers
{
    public static class OpenFormsHelpers
    {
        public static T GetFormInstance<T>() where T : Form
        {
            return IoC.GetForm<T>();
        }
        public static void OpenForm<TFrom>() where TFrom : Form
        {
            var form = IoC.GetForm<TFrom>();
            OpenForm(form);
        }
        
        public static void OpenForm(Form form)
        {
            form.Show();
        }
        public static void OpenFormMdi<TFrom>() where TFrom : Form
        {
            var form = IoC.GetForm<TFrom>();
            OpenFormMdi(form);
        }
        public static void OpenFormMdi(Form form)
        {
            //var frmMain = GetFormInstance<Views.FrmMain>();
            //form.MdiParent = frmMain;
            form.MdiParent = ApplicationContextMy.MainForm;            
            form.Show();
            
        }

        public static void OpenFormDialog<TFrom>() where TFrom : Form
        {
            //var form = Config.IoC.GetForm<TFrom>();
            var form = GetFormInstance<TFrom>();
            form.ShowDialog();
        }

        public static void OpenFormDialog(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
        public static TFrom OpenFormDialogReturn<TFrom>() where TFrom : Form
        {
            var form = GetFormInstance<TFrom>();
            form.ShowDialog();
            return form;
        }


    }
}
