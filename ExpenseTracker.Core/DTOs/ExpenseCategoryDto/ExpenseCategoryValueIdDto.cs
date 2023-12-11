using ExpenseTracker.Core.Configuration;
using ExpenseTracker.Core.DTOs.General;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.DTOs.ExpenseCategoryDto
{
    public class ExpenseCategoryValueIdDto : ValueIdDto
    {
        #region Singleton Design Pattern
        private ExpenseCategoryValueIdDto() { }
        // Lazy is Thread Safe Default
        private static readonly Lazy<ExpenseCategoryValueIdDto> instanceLock = new Lazy<ExpenseCategoryValueIdDto>(() => new ExpenseCategoryValueIdDto());
        public static ExpenseCategoryValueIdDto Instance
        {
            get
            {
                return instanceLock.Value;
            }
        }
        #endregion
        
        private IList<ExpenseCategoryMdl> lstData=new List<ExpenseCategoryMdl>();
        public List<ExpenseCategoryValueIdDto> GetAll(bool refresh = false)
        {
            return _GetAll(getAll(refresh));
        }
        private List<ExpenseCategoryValueIdDto> _GetAll(IList<ExpenseCategoryMdl> lstMdl)
        {
            var lst = new List<ExpenseCategoryValueIdDto>();
            foreach (var item in lstMdl)
            {
                lst.Add(new ExpenseCategoryValueIdDto() { id = item.id, name = item.name });
            }
            return lst;
        }

        private IList<ExpenseCategoryMdl> getAll(bool refresh = false)
        {
            if (lstData == null || lstData.Count == 0 || refresh)
            {
                IExpenseCategoryRepo _repo = DependenciesIOC.GetInstanceUC<IExpenseCategoryRepo>();
                var entities = _repo.GetAll().ToList();
                lstData = entities;
            }
            return lstData;
        }
    }
}
