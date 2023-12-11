using ExpenseTracker.UIWinForms.Config;
using ExpenseTracker.UIWinForms.Config.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.UIWinForms.DRY
{
    class OpenMainForm
    {
        
        public static void ShowSystem<TForm>(Form formClose) where TForm : Form
        {
            ThreadStart threadStart = start_Form_mainAccounting<TForm>;
            Thread thr = new Thread(threadStart);
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
            formClose.Close();
        }
        static void start_Form_mainAccounting<TForm>() where TForm : Form
        {
            //var frmMain = OpenFormsHelpers.GetFormInstance<MainAccountingForm>();
            var frmMain = OpenFormsHelpers.GetFormInstance<TForm>();
            ApplicationContextMy.MainForm = frmMain;
            //frmMain.Show();
            Application.Run(frmMain);
        }
    }
}
