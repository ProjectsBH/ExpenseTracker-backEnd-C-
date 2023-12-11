using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.DTOs.ExpenseDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.UIWinForms.Config.Helpers;
using ExpenseTracker.UIWinForms.DRY;
using ExpenseTracker.UIWinForms.Helper.ControlHelper;
using ExpenseTracker.UIWinForms.MyClasses;
using ExpenseTracker.UIWinForms.Views.Sub;
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
    public partial class ExpenseForm : Form
    {
        private readonly IExpenses useCase;
        private string title;
        private bool isAddOperation = true;
        public ExpenseForm(IExpenses _useCase)
        {
            InitializeComponent();
            useCase = _useCase;
            GetOthers();
            isAddOperation = true;
            btnSave.Text = "إضافة";
        }
       
        public ExpenseForm SetDI(ExpenseResponseDto data)
        {
            isAddOperation = false;
            btnSave.Text = "تعديل";
            SetData(data);
            return this;
        }
        void GetOthers()
        {
            GeneralVariables.isExecute = false;
            title = useCase.title;
            lblTitle.Text = title;
            GetData();
        }
        void GetData()
        {
            GetExpenseCategories();

        }
        void GetExpenseCategories()
        {
            ComboBoxHelper.SetCmbData(cmbCategory, ExpenseCategoryValueIdDto.Instance.GetAll,true);
        }

        void CancelOperation()
        {
            ToolsMyClass.clear(grpBoxData.Controls);
            IdEdit.Text = null;
        }
        ExpenseResponseDto dataRow;
        void SetData(ExpenseResponseDto data)
        {
            IdEdit.Text = data.id.ToString();
            cmbCategory.SelectedValue = data.categoryId;
            txtAmount.Text = data.amount.ToString();
            txtStatement.Text = data.theStatement;
            dateTimePck_theDate.Text = data.theDate.ToString();
           
            dataRow = data;
        }
        ExpenseRequestDto GetDisplacedDataRequestDto()
        {
            ExpenseRequestDto entity = new ExpenseRequestDto()
            {
                categoryId= Convert.ToInt32(cmbCategory.SelectedValue),
                amount = Convert.ToDecimal(txtAmount.Text),
                theDate =Convert.ToDateTime(dateTimePck_theDate.Value.ToShortDateString()),
                theStatement = txtStatement.Text,
            };
            return entity;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        bool ValidationUI(OperationType type = OperationType.add)
        {
            bool valueReturn = false;
            if (type == OperationType.edit)
            {
                int id;
                if (IdEdit.Text == null && !int.TryParse(IdEdit.Text, out id)) { MessageBox.Show("لا توجد مرجعية"); return valueReturn; }
            }

            if (ToolsMyClass.check(cmbCategory, "من فضلك إختار الفئة") == false) return valueReturn;
            if (ToolsMyClass.checkTrans(dateTimePck_theDate, "من فضلك حدد تاريخ العملية") == false) return valueReturn;
            if (ToolsMyClass.check(txtAmount, "من فضلك إدخل المبلغ") == false) return valueReturn;
            if (ToolsMyClass.check(txtStatement, "من فضلك إدخل البيان") == false) return valueReturn;
            
            if (type == OperationType.edit)
            {
                if (dataRow != null && txtAmount.Text.Trim() == dataRow.amount.ToString() && dateTimePck_theDate.Value == dataRow.theDate
                    && cmbCategory.SelectedValue.ToString() == dataRow.categoryId.ToString() && txtStatement.Text.Trim() == dataRow.theStatement)
                {
                    MessageBox.Show("لم يتم التعديل في البيانات");
                    return valueReturn;
                }
            }

            return true;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAddOperation)
                    Add();
                else
                    Edit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Add()
        {
            // Validation
            if (ValidationUI(OperationType.add) == false) return;

            // Call
            ExpenseRequestDto entity = GetDisplacedDataRequestDto();

            var result = useCase.Add(entity);

            // Show Result
            MessageBox.Show(result.Message, title);
            if (result.Success) // 10
            {
                GeneralVariables.isExecute = true;
                CancelOperation();
            }
            else if (result.ResultType != ResultsTypes.Incorrect_Input)
            {
                ToolsMyClass.SelectAll(txtAmount);
            }


        }
        private void Edit()
        {
            // Validation
            if (ValidationUI(OperationType.edit) == false) return;

            // Call
            long id = Convert.ToInt64(IdEdit.Text);
            ExpenseRequestDto entity = GetDisplacedDataRequestDto();

            var result = useCase.Edit(id, entity);

            // Show Result
            MessageBox.Show(result.Message, title);
            if (result.Success) // 10
            {
                GeneralVariables.isExecute = true;
                Close();
            }

        }
        private void btnAddIdentityType_Click(object sender, EventArgs e)
        {
            var frm = OpenFormsHelpers.GetFormInstance<AddExpenseCategoryForm>();
            frm.ShowDialog();
            if (GeneralVariables.isExecute)
            {
                GetExpenseCategories();
                cmbCategory.SelectedValue = frm.id;
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

        private void txtLimitAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckDRY.Amount_KeyPress(sender, e);
        }
        private void txtLimitAmount_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            CheckDRY.TextChanged(txt);
        }
    }
}
