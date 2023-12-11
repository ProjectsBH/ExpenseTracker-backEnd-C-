using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseGetAllRepo<T> where T : class, new()
    {
        // IQueryable<T> GetAll(); 
        IEnumerable<T> GetAll();
    }
}
