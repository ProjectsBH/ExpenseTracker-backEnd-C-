using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.UIWinForms.DRY;
using ExpenseTracker.UIWinForms.Helper.ControlHelper;
using ExpenseTracker.UIWinForms.Helper.ValuesHelper;
using ExpenseTracker.UIWinForms.MyClasses;
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
    public partial class ExpenseCategoryForm : Form
    {
        private readonly IExpenseCategory useCase;
        private string title;
        IList<ExpenseCategoryResponseDto> lstData = new List<ExpenseCategoryResponseDto>();
        public ExpenseCategoryForm(IExpenseCategory _useCase)
        {
            InitializeComponent();
            useCase = _useCase;
            
            GetOthers();
        }
        
        void GetOthers()
        {
            chBox_isLimitAmount.Checked = true;
            chBox_isLimitAmount.Checked = false;
            title = useCase.title;
            lblTitle.Text = title;
            GetRefreshDataDGV();
        }
        void GetRefreshDataDGV()
        {
            CancelOperation();
            try
            {
                lstData = useCase.GetAll();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            dGV.Columns.Clear();
            dGV.DataSource = lstData;
            DGV_Style();
        }
        void DGV_Style()
        {
            dGV.ColumnHeaderText(Pair.Of("id", "م"), Pair.Of("name", "اسم الفئة")
                , Pair.Of("isLimitAmountName", "حالة الحد"), Pair.Of("limitAmount", "مبلغ الحد"));

            dGV.ColumnHide("isLimitAmount", "created_by", "created_in");

            dGV.InsertColumnsForDGV("btnDelete", "حذف", "X");
            dGV.InsertColumnsForDGV("btnUpdate", "تعديل", "U", 1);

            dGV.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dGV.Columns["name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewColumn column2 = dGV.Columns["name"];
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void CancelOperation()
        {
            ToolsMyClass.StartShowWindow(grpBoxData, btnAdd, btnUpdate, btnNew);
            IdEdit.Text = null;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CancelOperation();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ToolsMyClass.BeforeAddUpdate(grpBoxData, txtName, btnAdd, btnUpdate, btnNew);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        bool ValidationUI(OperationType type = OperationType.add)
        {
            bool valueReturn = false;
            if (type == OperationType.edit)
            {
                int id;
                if (IdEdit.Text == null && !int.TryParse(IdEdit.Text, out id)) { MessageBox.Show("لا توجد مرجعية"); return valueReturn; }
            }

            if (ToolsMyClass.check(txtName, "من فضلك إدخل اسم الفئة") == false) return valueReturn;

            if (type == OperationType.edit)
            {
                ExpenseCategoryResponseDto dataRow = lstData.Where(a => a.id == Convert.ToInt32(IdEdit.Text)).SingleOrDefault() ?? null;
                if (dataRow != null && txtName.Text.Trim() == dataRow.name && chBox_isLimitAmount.Checked == dataRow.isLimitAmount
                    && txtLimitAmount.Text.Trim() == dataRow.limitAmount.ToString())
                {
                    MessageBox.Show("لم يتم التعديل في البيانات");
                    CancelOperation();
                    return valueReturn;
                }
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (ValidationUI(OperationType.add) == false) return;

                // Call
                ExpenseCategoryRequestDto mdl = new ExpenseCategoryRequestDto()
                {
                    name = txtName.Text.Trim(),
                    isLimitAmount = chBox_isLimitAmount.Checked ? true : false,
                    limitAmount = 0,
                };
                if (chBox_isLimitAmount.Checked)
                    mdl.limitAmount = Convert.ToDecimal(txtLimitAmount.Text.Trim());

                var result = useCase.Add(mdl);

                // Show Result
                MessageBox.Show(result.Message, title);
                if (result.Success) // 10
                {
                    GetRefreshDataDGV();
                }
                else if (result.ResultType != ResultsTypes.Incorrect_Input)
                {
                    // CancelOperation();
                    ToolsMyClass.SelectAll(txtName);
                }

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
                if (lstData.Count > 0 && dgv.Rows.Count > 0 && dgv.CurrentRow != null)
                {
                    if (e.ColumnIndex == dgv.Columns["btnDelete"].Index)
                    {
                        ExpenseCategoryResponseDto dataRow = lstData.GetEntity(a => a.id == dgv.GetIdFromDGV());
                        if (MessageBox.Show("هل تريد حذف " + title + " : " + dataRow.name + "  بالتأكيد ؟ ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            var result = useCase.Delete(dataRow.id);
                            MessageBox.Show(result.Message, "delete");
                            if (result.Success)
                            {
                                GetRefreshDataDGV();
                            }

                        }
                    }
                    else if (e.ColumnIndex == dgv.Columns["btnUpdate"].Index)
                    {
                        ToolsMyClass.BeforeAddUpdate(grpBoxData, txtName, btnUpdate, btnAdd, btnNew);
                        ExpenseCategoryResponseDto dataRow = lstData.GetEntity(a => a.id == dgv.GetIdFromDGV());
                        IdEdit.Text = dataRow.id.ToString();
                        txtName.Text = dataRow.name;
                        chBox_isLimitAmount.Checked=dataRow.isLimitAmount;
                        txtLimitAmount.Text = dataRow.limitAmount.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (ValidationUI(OperationType.edit) == false) return;

                // Call
                int id = Convert.ToInt32(IdEdit.Text);
                ExpenseCategoryRequestDto mdl = new ExpenseCategoryRequestDto()
                {
                    name = txtName.Text.Trim(),
                    isLimitAmount = chBox_isLimitAmount.Checked?true:false,
                    limitAmount = 0,
                };
                if (chBox_isLimitAmount.Checked)
                    mdl.limitAmount = Convert.ToDecimal(txtLimitAmount.Text.Trim());

                var result = useCase.Edit(id, mdl);

                MessageBox.Show(result.Message, title);
                if (result.Success)
                {
                    GetRefreshDataDGV();
                }
                else if (result.ResultType != ResultsTypes.Incorrect_Input)
                {
                    CancelOperation();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

        private void chBox_isLimitAmount_CheckedChanged(object sender, EventArgs e)
        {
            var chckBox = sender as CheckBox;
            BackgroundChangeCheckBox(chckBox);
            //txtLimitAmount.Enabled = chckBox.Checked == true ? true : false;
            txtLimitAmount.Enabled = chckBox.Checked;
        }
        private void BackgroundChangeCheckBox(CheckBox chckBox)
        {
            ToolsMyClass.BackgroundChangeCheckBox_CheckedChanged(chckBox);
        }
        private void txtLimitAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckDRY.Amount_KeyPress(sender, e);
        }
        private void txtLimitAmount_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            CheckDRY.TextChanged(txt);
        }

        private void dGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == dGV.Columns["limitAmount"].Index)
                {
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        double d = Math.Abs(double.Parse(CheckDRY.CheckNullOrEmpty(e.Value.ToString()).ToString()));
                        e.Value = d.ToString("#,##0.##");
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
