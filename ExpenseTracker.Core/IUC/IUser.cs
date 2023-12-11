using ExpenseTracker.Core.DTOs.UserDto;
using ExpenseTracker.Core.Utils.FinalResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.IUC
{
    public interface IUser
    {
        ServicesResultsDto Login(string userName, string password);
        ServicesResultsDto SignUp(UserRequestDto entity);
    }
}
