using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.UIWinForms.Config
{
    public static class ApplicationContextMy
    {
        public static Form MainForm { get; set; }

        public static string Username { get; set; } = string.Empty;
    }
}
