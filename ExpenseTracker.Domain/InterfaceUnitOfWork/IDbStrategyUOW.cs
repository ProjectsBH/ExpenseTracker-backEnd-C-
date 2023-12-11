using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.InterfaceUnitOfWork
{
    public interface IDbStrategyUOW : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void CreateTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
