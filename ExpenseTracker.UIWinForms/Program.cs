using ExpenseTracker.UIWinForms.Config.Helpers;

namespace ExpenseTracker.UIWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Config.IoC.Init();
            //var frmMain = Config.IoC.GetForm<FrmMain>();
            //var frmLogin = OpenFormsHelpers.GetFormInstance<Views.ExpenseCategoryForm>();
            var frmLogin = OpenFormsHelpers.GetFormInstance<Form_splash>();

            Application.Run(frmLogin);
            //Application.Run(new Form_splash());
        }
    }
}