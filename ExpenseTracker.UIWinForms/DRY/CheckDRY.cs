using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.DRY
{
    public static class CheckDRY
    {
        public static object CheckNullOrEmpty(object value)
        {
            if (value == null) value = 0;
            object s = string.IsNullOrEmpty(value.ToString()) ? 0 : value;
            return s;
        }
        public static void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char DecimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != DecimalSeparator)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Amount_Leave(TextBox txtAmount)
        {
            double d;
            if (double.TryParse(txtAmount.Text, out d))
                //txtAmount.Text = string.Format("{0:N2}", d);
                txtAmount.Text = d.ToString("###0.##");
        }

        public static void TextChanged(TextBox txtAmount)
        {
            try
            {
                string value;
                //value = txtAmount.Text.TrimStart('0').TrimStart('.');
                value = txtAmount.Text.Trim();
                txtAmount.Text = value.Trim();
                decimal ul;
                if (decimal.TryParse(value, out ul))
                {

                    if (value.IndexOf(".") == value.Length - 1 && !value.Remove(value.Length - 1).Contains("."))
                        txtAmount.Text = value;
                    else if (value.Length > 1 && value.IndexOf(".0") == value.Length - 2)
                    { txtAmount.Text = value; return; }
                    else
                    {
                        txtAmount.Text = ul.ToString("#,##0.##"); // txtAmount.Text = ul.ToString("#,##0.##");
                        value = ul.ToString();
                    }

                    txtAmount.Select(txtAmount.Text.Length, 0);

                }
                else
                {
                    if (value.Length > 1)
                    {
                        if (value.LastIndexOf(".") == value.Length - 1)
                            value = value.Remove(value.Length - 1);
                        txtAmount.Text = Convert.ToDecimal(value).ToString("#,##0.##");
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
