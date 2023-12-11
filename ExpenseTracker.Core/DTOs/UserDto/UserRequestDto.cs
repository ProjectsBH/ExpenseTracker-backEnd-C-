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
    public class UserRequestDto
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string email { get; set; }
        internal UserMdl toModel(UserRequestDto dto)
        {
            return new UserMdl()
            {
                userName = dto.userName,
                password = dto.password,
                email = dto.email,
                created_in = MyDateTime.GetDateTime()
            };
        }
        internal UserMdl toModel(int id, UserRequestDto dto)
        {
            return new UserMdl()
            {
                id = id,
                userName = dto.userName,
                password = dto.password,
                email = dto.email,
            };
        }
    }
}
