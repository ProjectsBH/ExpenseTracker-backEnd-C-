using ExpenseTracker.Core.IUC;
using ExpenseTracker.UIWinForms.DRY;
using ExpenseTracker.UIWinForms.MyClasses;
using System.Windows.Forms;
using System;
using ExpenseTracker.Shared.Infrastructure.Settings;

namespace ExpenseTracker.UIWinForms
{
    public partial class Form_Login : Form
    {
        private readonly IUser useCase;

        public Form_Login(IUser _user)
        {
            InitializeComponent();           
            useCase = _user;
            GetOthers();
        }
        void GetOthers()
        {
            GeneralVariables.setAppName(lblAppName);
            lblProviderName.Text = AppSettings.DBProvider.ToString();
            DefaultUser();
        }
        private void DefaultUser()
        {
            if (Properties.Settings.Default.userName.Trim().Length > 0)
            {
                txtName.Text = Properties.Settings.Default.userName.Trim();
                txtPwd.TabIndex = 0;
                txtName.TabStop = false;
                //txtPwd.Text = "123";
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                //
                if (ToolsMyClass.check(txtName, "Ì—ÃÏ «œŒ· «”„ «·„” Œœ„") == false) return;
                if (ToolsMyClass.check(txtPwd, "Ì—ÃÏ «œŒ· ﬂ·„… «·„—Ê—") == false) return;
                //

                var result = useCase.Login(txtName.Text.Trim(), txtPwd.Text.Trim());

                if (result.ResultType == ResultsTypes.Exception)
                {
                    MessageBox.Show(result.Message);
                }
                else if (result.Success)
                {
                    if (Properties.Settings.Default.userName.Trim() != txtName.Text.Trim())
                    {
                        Properties.Settings.Default.userName = txtName.Text.Trim();
                        Properties.Settings.Default.Save();
                    }
                    OpenMainForm.ShowSystem< Views.MainForm> (this);
                   

                }
                else
                {
                    MessageBox.Show("»Ì«‰«   ”ÃÌ· «·œŒÊ· €Ì— ’ÕÌÕ…", " ”ÃÌ· «·œŒÊ·");
                    ToolsMyClass.clear(this.grBoxControl.Controls);
                    ToolsMyClass.focus(txtName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //  Õ—Ìﬂ «·Ê«ÃÂ… »«·„«Ê”
        private Point _mouseLoc;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            FormMouseMove.MouseMoveHeader(this, _mouseLoc, e);
        }

    }
}