using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.Core.Utils;
using ExpenseTracker.UIWinForms.Config.Helpers;
using ExpenseTracker.UIWinForms.DRY;
using ExpenseTracker.UIWinForms.Helper.ControlHelper;
using ExpenseTracker.UIWinForms.Helper.ValuesHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.Views
{
    public partial class MainForm : Form
    {
        private readonly IExpenses mainUC;
        public MainForm(IExpenses _mainUC)
        {
            InitializeComponent();
            mainUC = _mainUC;
        }
        
        private void Form_main_Load(object sender, EventArgs e)
        {
            GetOthers();
        }
        void GetOthers()
        {
            GeneralVariables.setAppName(lblAppName);
            GetRefreshDataDGV();
        }


        IList<ExpenseResponseDto> GetData()
        {
            return mainUC.GetTop();
        }
        void GetRefreshDataDGV()
        {
            try
            {
                
                IList<ExpenseResponseDto> lstData = GetData();

                dGV.Columns.Clear();
                dGV.DataSource = lstData;
                dGV.ColumnHeaderText(Pair.Of("id", "المعرف"), Pair.Of("categoryName", "الفئة")
               , Pair.Of("amount", "المبلغ"), Pair.Of("theStatement", "البيان")
               , Pair.Of("theDate", "التاريخ"), Pair.Of("created_in", "تاريخ الإضافة"));

                dGV.ColumnHide("id", "categoryId", "created_by", "created_in");

                dGV.InsertColumnsForDGV("btnDelete", "حذف", "X");
                dGV.InsertColumnsForDGV("btnUpdate", "تعديل", "U", 1);

                dGV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dGV.Columns["theStatement"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                DataGridViewColumn column2 = dGV.Columns["theStatement"];
                column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                lblCount.Text = lstData.Count.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv.Rows.Count > 0 && dgv.CurrentRow != null)
                {
                    if (e.ColumnIndex == dgv.Columns["btnUpdate"].Index)
                    {
                        IList<ExpenseResponseDto> lstData = (List<ExpenseResponseDto>)dgv.DataSource;
                        ExpenseResponseDto data = lstData.GetEntity(a => a.id == dgv.GetId64FromDGV());

                        OpenFormsHelpers.GetFormInstance<ExpenseForm>().SetDI(data).ShowDialog();
         
                        if (GeneralVariables.isExecute)
                            GetRefreshDataDGV();
                    }
                    else if (e.ColumnIndex == dgv.Columns["btnDelete"].Index)
                    {
                        int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["id"].Value);

                        if (MessageBox.Show("هل تريد حذف بيانات العملية رقم : " + id + "  بالتأكيد ؟ ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            var result = mainUC.Delete(id);
                            MessageBox.Show(result.Message, "delete");
                            if (result.Success)
                            {
                                GetRefreshDataDGV();
                            }
                        }

                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        

        #region Open Form
        void OpenFormDialog(Form frm)
        {
            OpenFormsHelpers.OpenFormDialog(frm);
        }
        void OpenFormDialog<T>() where T : Form
        {
            OpenFormsHelpers.OpenFormDialog<T>();
        }
        #endregion

        #region Menus 


        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            OpenFormDialog<ExpenseForm>();
            if (GeneralVariables.isExecute)
                GetRefreshDataDGV();
        }

        private void btnExpenseCategory_Click(object sender, EventArgs e)
        {
            OpenFormDialog<ExpenseCategoryForm>();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenFormDialog<ExpensesReportForm>();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenFormDialog<ExpensesSearchForm>();
        }
        private void btnPrivacy_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Header 
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // تحريك الواجهة بالماوس
        private Point _mouseLoc;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            FormMouseMove.MouseMoveHeader(this, _mouseLoc, e);
        }



        #endregion

        private void dGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                
                if (e.ColumnIndex == dGV.Columns["amount"].Index)
                {
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        double d = Math.Abs(double.Parse(CheckDRY.CheckNullOrEmpty(e.Value.ToString()).ToString()));
                        e.Value = d.ToString("#,###.##");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }

}
