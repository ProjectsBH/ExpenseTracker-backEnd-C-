namespace ExpenseTracker.Domain.IRepository.BaseIRepository
{
    public interface IBaseGetByIdRepo<T> where T : class, new()
    {
        T GetById(object id);
    }
}
