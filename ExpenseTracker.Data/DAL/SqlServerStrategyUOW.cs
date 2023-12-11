using ExpenseTracker.Domain.InterfaceUnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.DAL
{
    public class SqlServerStrategyUOW : IDbStrategyUOW
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction = null;
        private bool _disposed;

        public SqlServerStrategyUOW(IDbConnection connection)
        {
            _connection = connection;
        }


        public IDbConnection Connection
        {
            get { return _connection; }
        }
        //public IDbConnection GetConnection()
        //{
        //    return _connection;
        //}
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }
        public void CreateTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await Task.Run(() => _connection.BeginTransaction());
            //_transaction = await _connection.BeginTransactionAsync();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public async Task CommitAsync()
        {
            await Task.Run(() => _transaction.Commit());
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        public async Task RollbackAsync()
        {
            await Task.Run(() =>
            {
                _transaction.Rollback();
                _transaction.Dispose();
            });
        }
        public void Dispose()
        {
            //_transaction?.Dispose();
            //_connection.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _connection.Dispose();
            _disposed = true;
        }


    }
}
