using ExpenseTracker.Core.DTOs.ExpenseCategoryDto;
using ExpenseTracker.Core.Utils;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ExpenseTracker.Core.DTOs.ExpenseDto
{
    public class ExpenseResponseDto
    {
        public long id { get; set; }
        public int categoryId { get; set; }       
        public DateTime theDate { get; set; }
        public decimal amount { get; set; }
        public string categoryName { get; set; }
        public string theStatement { get; set; }
        public DateTime created_in { get; set; }
        public int created_by { get; set; }


        private ExpenseResponseDto fromModel(ExpensesMdl dto)
        {
            if (dto == null) return null;
            return new ExpenseResponseDto()
            {
                id = dto.id,
                categoryId = dto.categoryId,
                theDate = dto.theDate,
                amount = dto.amount,
                theStatement = dto.theStatement,
                created_in = dto.created_in,
                created_by = dto.created_by,
                //categoryName = lstCategory.GetValueStr(a => a.id == dto.categoryId
                //    , PropertyHelper.GetPropertyName((ExpenseCategoryValueIdDto v) => v.name)),
            };
        }
        internal ExpenseResponseDto fromModelSearch(ExpensesMdl dto)
        {
            if (dto == null) return null;
            var item = fromModel(dto);
            item.categoryName = getCategories().GetValueStr(a => a.id == dto.categoryId
                    , PropertyHelper.GetPropertyName((ExpenseCategoryValueIdDto v) => v.name));
            return item;
        }
        internal ExpenseResponseDto fromModel(ExpensesMdl dto,List<ExpenseCategoryValueIdDto> lstCategory)
        {
            var item = fromModel(dto);
            item.categoryName = lstCategory.GetValueStr(a => a.id == dto.categoryId
                    , PropertyHelper.GetPropertyName((ExpenseCategoryValueIdDto v) => v.name));
            return item;
        }
        internal List<ExpenseResponseDto> fromModel(List<ExpensesMdl> dto)
        {
            var lstCategory = getCategories();
            var lst = new List<ExpenseResponseDto>();
            lst.AddRange(dto.Select((x) => fromModel(x, lstCategory)));
            return lst;
        }
        private List<ExpenseCategoryValueIdDto> getCategories() => ExpenseCategoryValueIdDto.Instance.GetAll();
    }
}
