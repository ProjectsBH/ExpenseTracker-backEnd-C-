using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Shared.DRY;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs.ExpenseDto
{
    public class ExpenseRequestDto
    {
        public int categoryId { get; set; }
        public DateTime theDate { get; set; }
        public decimal amount { get; set; }
        public string theStatement { get; set; }
        public int userId { get; set; }
        internal ExpensesMdl toModel(ExpenseRequestDto dto)
        {
            return new ExpensesMdl()
            {
                categoryId = dto.categoryId,
                theDate = dto.theDate,
                amount = dto.amount,
                theStatement = dto.theStatement,
                created_by = SessionData.idUserMy,
                created_in = DateTime.UtcNow
            };
        }
        internal ExpensesMdl toModel(long id, ExpenseRequestDto dto)
        {
            return new ExpensesMdl()
            {
                id = id,
                categoryId = dto.categoryId,
                theDate = dto.theDate,
                amount = dto.amount,
                theStatement = dto.theStatement,
            };
        }
    }
}
