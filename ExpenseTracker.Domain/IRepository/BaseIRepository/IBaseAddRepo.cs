using System;


namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseAddRepo<T> where T : class, new()
    {
        Tuple<bool, object> Add(T entity);
    }
}
