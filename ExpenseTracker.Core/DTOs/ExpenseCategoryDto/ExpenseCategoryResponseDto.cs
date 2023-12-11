using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs.ExpenseCategoryDto
{
    public class ExpenseCategoryResponseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isLimitAmount { get; set; }
        public decimal limitAmount { get; set; }
        public DateTime created_in { get; set; }
        public int created_by { get; set; }
        public string isLimitAmountName { get; set; }


        internal ExpenseCategoryResponseDto fromModel(ExpenseCategoryMdl dto)
        {
            if (dto == null) return null;
            return new ExpenseCategoryResponseDto()
            {
                id = dto.id,
                name = dto.name,
                isLimitAmount = dto.isLimitAmount,
                limitAmount = dto.limitAmount,
                created_in = dto.created_in,
                created_by = dto.created_by,
                isLimitAmountName = dto.isLimitAmount ? "موجود" : "لا يوجد"
            };
        }
        internal List<ExpenseCategoryResponseDto> fromModel(List<ExpenseCategoryMdl> dto)
        {
            var lst = new List<ExpenseCategoryResponseDto>();
            lst.AddRange(dto.Select((x) => fromModel(x)));
            return lst;
        }
    }
}
