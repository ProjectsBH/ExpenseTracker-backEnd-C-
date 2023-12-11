using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataMySQL.CustomException
{
    public class DALFundsException : Exception
    {
        public DALFundsException()
        {
        }

        public DALFundsException(string message)
            : base(message)
        {
        }

        public DALFundsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
