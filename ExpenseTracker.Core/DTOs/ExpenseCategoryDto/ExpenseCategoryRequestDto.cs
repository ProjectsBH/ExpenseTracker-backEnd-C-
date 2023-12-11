using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs.ExpenseCategoryDto
{
    public class ExpenseCategoryRequestDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "اسم الفئة")]
        public string name { get; set; }
        public bool isLimitAmount { get; set; }
        public decimal limitAmount { get; set; }
        public int userId { get; set; } 
        internal ExpenseCategoryMdl toModel(ExpenseCategoryRequestDto dto)
        {
            return new ExpenseCategoryMdl()
            {
                name = dto.name,
                isLimitAmount = dto.isLimitAmount,
                limitAmount = dto.limitAmount,
                created_by = SessionData.idUserMy,
                created_in = DateTime.UtcNow
            };
        }
        internal ExpenseCategoryMdl toModel(int id, ExpenseCategoryRequestDto dto)
        {
            return new ExpenseCategoryMdl()
            {
                id = id,
                name = dto.name,
                isLimitAmount = dto.isLimitAmount,
                limitAmount = dto.limitAmount,
            };
        }
    }
}
