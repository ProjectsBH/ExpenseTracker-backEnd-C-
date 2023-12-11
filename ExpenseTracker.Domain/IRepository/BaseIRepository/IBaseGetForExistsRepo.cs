

namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseGetForExistsRepo<T> where T : class, new()
    {
        T GetForExists(string name, object id = null);
    }
}
