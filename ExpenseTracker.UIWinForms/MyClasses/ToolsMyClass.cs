using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.MyClasses
{
    class ToolsMyClass
    {
        // تنضيف التيكست بوكس والكمبوبوكس
        public static void clear(Control.ControlCollection cont, TextBox focus_te = null)
        {
            foreach (Control item in cont) // treat public tools class
            {
                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Clear();

                else if (item.GetType() == typeof(ComboBox))
                    ((ComboBox)item).SelectedIndex = -1;


                else if (item.GetType() == typeof(DateTimePicker))
                    ((DateTimePicker)item).ResetText();

                else if (item.GetType() == typeof(CheckBox))
                    ((CheckBox)item).Checked = false;

                if (focus_te != null)
                    focus_te.Focus();
            }
        }


        // تنضيف التيكست بوكس والكمبوبوكس
        public static void enabledContr(Control.ControlCollection cont, bool value = true)
        {
            foreach (Control item in cont)
            {
                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Enabled = value;

                else if (item.GetType() == typeof(ComboBox))
                    ((ComboBox)item).Enabled = value;

                else if (item.GetType() == typeof(DateTimePicker))
                    ((DateTimePicker)item).Enabled = value;

                else if (item.GetType() == typeof(RadioButton))
                    ((RadioButton)item).Enabled = value;

                else if (item.GetType() == typeof(CheckBox))
                    ((CheckBox)item).Enabled = value;

                else if (item.GetType() == typeof(Panel))
                    ((Panel)item).Enabled = value;

                else if (item.GetType() == typeof(Button))
                {
                    ((Button)item).Enabled = value;
                    btn(((Button)item), value);
                }

            }
        }
        public static void enabled(Control cont, bool value = true)
        {
            cont.Enabled = value;
        }
        public static void trim(Control.ControlCollection cont)
        {
            foreach (Control item in cont)
            {
                if (item.GetType() == typeof(TextBox))
                    ((TextBox)item).Text.Trim();
            }
        }


        //التشييك انه ادخل بيانات في الاداة
        public static bool check(TextBox txt, string Maseege)
        {
            // if (txt == null || txt.Text=="" || txt.TextLength==0)
            // ثم هل نوعه نصوص صحيحة
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show(Maseege, "خطأ");
                focus(txt);
                return false;
            }
            return true;
        }
        // يقبل تاريخ اليوم وتاريخ قديم فقط ولا يقبل غدوه وما بعده
        public static bool checkTrans(DateTimePicker dTPckr, string Maseege)
        {
            try
            {
                //var n = Convert.ToDateTime(DateTime.TryParse(dTPckr.Text.Trim(), out DateTime i));
                if (string.IsNullOrWhiteSpace(dTPckr.Text))
                {
                    ReturnErrorsCheckDateTime(dTPckr, "من فضلك ادخل " + Maseege);
                    return false;
                }
                else if (Convert.ToDateTime(dTPckr.Text.Trim()) > DateTime.Now.Date)
                {
                    ReturnErrorsCheckDateTime(dTPckr, Maseege + " المحدد اكبر من التايخ الحالي");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckDateTime(dTPckr, Maseege + "\n" + "التاريخ غير صالح");
                return false;
            }
        }
        // يقبل اي تاريخ الحالي او قبله او بعده
        public static bool check(DateTimePicker dTPckr, string Maseege)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dTPckr.Text))
                {
                    //"من فضلك ادخل تاريخ الحركة"
                    ReturnErrorsCheckDateTime(dTPckr, "من فضلك ادخل " + Maseege);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckDateTime(dTPckr, Maseege + "\n" + "التاريخ غير صالح");
                return false;
            }
        }
        // يقبل تاريخ اليوم وتاريخ جديد فقط ولا يقبل أمس وما قبله
        public static bool checkNew(DateTimePicker dTPckr, string Maseege)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dTPckr.Text))
                {
                    ReturnErrorsCheckDateTime(dTPckr, "من فضلك ادخل " + Maseege);
                    return false;
                }
                else if (Convert.ToDateTime(dTPckr.Text.Trim()) < DateTime.Now.Date)
                {
                    ReturnErrorsCheckDateTime(dTPckr, Maseege + " المحدد أصغر من التايخ الحالي");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckDateTime(dTPckr, Maseege + "\n" + "التاريخ غير صالح");
                return false;
            }
        }
        static void ReturnErrorsCheckDateTime(DateTimePicker dTPckr, string Maseege)
        {
            MessageBox.Show(Maseege, "خطأ");
            focus(dTPckr);
        }
        // اذا كان الانبت مخصص للأرقام يختبر هل هو فارغ او اصغر من واحد 1 والزامي ندخل القيمة وكرقم اكبر من صفر       
        public static bool check_num(TextBox Text, string Maseege)
        {
            try
            {
                //long i;
                long n = Convert.ToInt64(long.TryParse(Text.Text.Trim(), out long i));
                if (string.IsNullOrWhiteSpace(Text.Text) || i < 1)
                {
                    ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "أرقام فقط");
                    return false;
                }
                Text.Text = i.ToString();
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "أرقام اكبر من صفر");
                return false;
            }
        }
        static void ReturnErrorsCheckTextBox(TextBox Text, string Maseege)
        {
            MessageBox.Show(Maseege, "خطأ");
            Text.SelectAll();
            focus(Text);
        }
        // يكون مبلغ وليس اصغر من الواحد
        public static bool check_decimal(TextBox Text, string Maseege)
        {
            try
            {

                decimal i;
                decimal n = Convert.ToDecimal(decimal.TryParse(Text.Text.Trim(), out i));
                if (string.IsNullOrWhiteSpace(Text.Text) || i < 1)
                //if (string.IsNullOrWhiteSpace(Text.Text) || Convert.ToDecimal(decimal.TryParse(Text.Text.Trim(), out i)) < 1) // لو ترحل المبلغ صفر 0 يعدل رصيد الحساب الى فارغ نل
                {
                    ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "يكون مبلغ مالي صحيح اكبر او يساوي الواحد");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "مبلغ اكبر او يساوي الواحد");
                return false;
            }
        }
        // يكون مبلغ أكبر من الصفر
        public static bool check_decimalGreaterThanZero(TextBox Text, string Maseege)
        {
            try
            {

                decimal i;
                decimal n = Convert.ToDecimal(decimal.TryParse(Text.Text.Trim(), out i));
                if (string.IsNullOrWhiteSpace(Text.Text) || i <= 0)
                {
                    ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "يكون مبلغ مالي صحيح اكبر من الصفر");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "مبلغ اكبر من الصفر");
                return false;
            }
        }
        // يكون مبلغ من الصفر وما فوق
        public static bool check_decimalWithZero(TextBox Text, string Maseege)
        {
            decimal i;
            decimal n = Convert.ToDecimal(decimal.TryParse(Text.Text.Trim(), out i));
            if (string.IsNullOrWhiteSpace(Text.Text) || i < 0)
            {
                ReturnErrorsCheckTextBox(Text, Maseege + "\n" + "يكون مبلغ مالي صحيح");
                return false;
            }
            return true;
        }
        public static bool check(ComboBox cmb, string Maseege)
        {
            if (cmb.Items.Count > 0)
            {
                if (cmb.SelectedIndex == -1)
                {
                    MessageBox.Show(Maseege, "خطأ");
                    focus(cmb);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(Maseege + "\n أولا قم بالأضافة ثم إختار", "خطأ");
                focus(cmb); return false;
            }
            return true;
        }

        public static bool check(ComboBox cmb)
        {
            if (cmb.Items.Count > 0)
            {
                if (cmb.SelectedIndex >= 0)
                {
                    return true;
                }
                // else return false;
            }
            else
            {
                // MessageBox.Show(Maseege + "\n أولا قم بالأضافة ثم إختار", "خطأ");
                focus(cmb); return false;
            }
            focus(cmb); return false;
        }


        //وضع التركيز في الاداة المحددة
        public static void focus(TextBox focus_te)
        {
            focus_te.Focus();
        }
        public static void SelectAll(TextBox txt)
        {
            txt.SelectAll();
            focus(txt);

        }
        public static void focus(Control item)
        {
            if (item.GetType() == typeof(TextBox))
                ((TextBox)item).Focus();

            else if (item.GetType() == typeof(ComboBox))
                ((ComboBox)item).Focus();
            // focus_te.Focus();
        }

        // قيم التاريخ الثاني لازم تكون اقل او يساوي من تاريخ البداية وتتغير بتغير تاريخ البداية
        public static void toDate_ValueChanged(DateTimePicker fromDate, DateTimePicker toDate)
        {
            toDate.MinDate = fromDate.Value.Date;
            if (Convert.ToDateTime(fromDate.Value) > Convert.ToDateTime(toDate.Value))
                toDate.Text = fromDate.Text;
        }
        // يضيف اصفار او ما شئت من رموز قبل السلسلة مثل 002
        public static string PadString(string str, int num = 6, char pad = '0')
        {
            return str.PadLeft(num, pad);
            //string d = "";
            //d = str.PadLeft(num, pad);
            //return d;
        }

        // تفعيل الزر مع تغيير لون الخط
        // إلغاء تفعيل الزر مع تغيير لون الخط
        public static void btn(Button btn, bool activate = true)
        {
            btn.BackColor = activate == true ? Color.Navy : Color.Gray;
            //btn.ForeColor = activate == true ? Color.White : Color.Gray; //Color.DimGray //DarkGray
            btn.Enabled = activate;
        }

        //  بداية ظهور الواجهة ثم وبعد الاضافة او التعديل
        public static void StartShowWindow(GroupBox gr, Button btnAdd = null, Button btnUpdate = null, Button btnNew = null)
        {
            clear(gr.Controls);
            enabled(gr, false);
            // or
            //Control.ControlCollection item = gr.Controls;
            //clear(item);
            //enabledContr(item, false);
            if (btnAdd != null) { btn(btnAdd, false); } // إلغاء تفعيل ازرار الاضافة والتعديل و تغير الخط للزرين كمان
            if (btnUpdate != null) { btn(btnUpdate, false); } // إلغاء تفعيل ازرار الاضافة والتعديل و تغير الخط للزرين كمان
            if (btnNew != null) { btn(btnNew); } // تفعيل زر جديد نستخدم في الرجوع
        }
        // يمر على كل أداء على حده
        public static void StartShowWindowPerControl(GroupBox gr, Button btnAdd = null, Button btnUpdate = null, Button btnNew = null)
        {
            Control.ControlCollection item = gr.Controls;
            clear(item);
            enabledContr(item, false);
            if (btnAdd != null) { btn(btnAdd, false); } // إلغاء تفعيل ازرار الاضافة والتعديل و تغير الخط للزرين كمان
            if (btnUpdate != null) { btn(btnUpdate, false); } // إلغاء تفعيل ازرار الاضافة والتعديل و تغير الخط للزرين كمان
            if (btnNew != null) { btn(btnNew); } // تفعيل زر جديد نستخدم في الرجوع
        }
        // الشروع او التهيؤ للاضافة او للتعديل    
        public static void BeforeAddUpdate(GroupBox gr, TextBox focus_te = null, Button btnIsEnabled = null, Button btnNoIsEnabled = null, Button btnNew = null)
        {
            clear(gr.Controls);
            enabled(gr);
            focus(focus_te);
            // or
            //Control.ControlCollection item = gr.Controls;
            //enabledContr(item);
            //clear(item, focus_te);
            if (btnIsEnabled != null) { btn(btnIsEnabled); } // تفعيل زر الاضافة عند الاضافة والتعديل عند التعديل
            if (btnNoIsEnabled != null) { btn(btnNoIsEnabled, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
            if (btnNew != null) { btn(btnNew, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
        }
        // يمر على كل أداء على حده
        public static void BeforeAddUpdatePerControl(GroupBox gr, TextBox focus_te = null, Button btnIsEnabled = null, Button btnNoIsEnabled = null, Button btnNew = null)
        {
            Control.ControlCollection item = gr.Controls;
            enabledContr(item);
            clear(item, focus_te);
            if (btnIsEnabled != null) { btn(btnIsEnabled); } // تفعيل زر الاضافة عند الاضافة والتعديل عند التعديل
            if (btnNoIsEnabled != null) { btn(btnNoIsEnabled, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
            if (btnNew != null) { btn(btnNew, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
        }
        public static void BeforeAddUpdate(GroupBox gr, ComboBox focus_cmb, Button btnIsEnabled = null, Button btnNoIsEnabled = null, Button btnNew = null)
        {
            clear(gr.Controls);
            enabled(gr);
            // or
            //Control.ControlCollection item = gr.Controls;
            //enabledContr(item);
            //clear(item, null);
            focus(focus_cmb);
            if (btnIsEnabled != null) { btn(btnIsEnabled); } // تفعيل زر الاضافة عند الاضافة والتعديل عند التعديل
            if (btnNoIsEnabled != null) { btn(btnNoIsEnabled, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
            if (btnNew != null) { btn(btnNew, false); } // إلغاء زر جديد واحد زري الاضافة او التعديل
        }

        // حدث تغير خلفية اداة CheckBox
        // عند اختيارها
        public static void BackgroundChangeCheckBox_CheckedChanged(CheckBox chckBox)
        {
            chckBox.BackColor = chckBox.Checked ? Color.FromArgb(255, 128, 128) : Color.Gray;
            //chckBox.BackColor = chckBox.Checked ? Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))) : Color.Gray;
        }

    }
}
