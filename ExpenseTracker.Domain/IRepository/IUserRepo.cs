using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.IRepository
{
    public interface IUserRepo
    {
        Tuple<bool, UserMdl?> Login(string userName, string password);
        Tuple<bool, object> SignUp(UserMdl entity);
        IEnumerable<UserMdl> GetDuplicate(string userName, string password);
    }
}
