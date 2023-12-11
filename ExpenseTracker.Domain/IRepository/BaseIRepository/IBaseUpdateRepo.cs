

namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseUpdateRepo<T> where T : class, new()
    {
        bool Update(T entity);
    }
}
