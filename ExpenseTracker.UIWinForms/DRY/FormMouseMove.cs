using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker.UIWinForms.DRY
{
    class FormMouseMove
    {
        public static void MouseMoveHeader(Form frm, Point _mouseLoc, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                frm.Location = new Point(frm.Location.X + dx, frm.Location.Y + dy);
            }
        }
    }
}
