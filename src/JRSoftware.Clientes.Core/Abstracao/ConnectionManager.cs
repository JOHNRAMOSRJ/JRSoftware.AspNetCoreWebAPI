using System;
using System.Data;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public interface IConnectionManager
	{
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; }
		IDbTransaction BeginTransaction();
		void EndTransaction(bool commit);
	}

	public class ConnectionManager<TIDbConnection> : IConnectionManager where TIDbConnection : IDbConnection, new()
	{
		private readonly IDbConnection _iDbConnection;
		private IDbTransaction _iDbTransaction = null;

		IDbConnection IConnectionManager.Connection => _iDbConnection;
		IDbTransaction IConnectionManager.Transaction => _iDbTransaction;

		public ConnectionManager(string connectionString, Action setup = null)
		{
			setup?.Invoke();
			_iDbConnection = new TIDbConnection() { ConnectionString = connectionString };
			_iDbConnection.Open();
		}

		public void Close()
		{
			_iDbConnection?.Close();
		}

		public IConnectionManager Open()
		{
			_iDbConnection?.Open();
			return this;
		}

		public void Dispose()
		{
			_iDbConnection?.Dispose();
		}

		IDbTransaction IConnectionManager.BeginTransaction()
		{
			return _iDbTransaction = _iDbConnection.BeginTransaction();
		}

		void IConnectionManager.EndTransaction(bool commit) => EndTransaction(_iDbTransaction, commit);

		private void EndTransaction(IDbTransaction dbTransaction, bool commit)
		{
			if (commit)
				dbTransaction.Commit();
			else
				dbTransaction.Rollback();
		}
	}
}