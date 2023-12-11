

namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseDeleteRepo<T> where T : struct
    {
        bool Delete(T entity_id);
    }
}
