using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs.UserDto
{
    public class LoginDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        
    }
}
