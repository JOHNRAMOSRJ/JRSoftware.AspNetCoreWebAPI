using System;
using System.Data;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public interface IConnectionManager: IDisposable
	{
		void Open();
		void Close();
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; }

		void BeginTransaction();
		void EndTransaction(bool commit);
	}

	public class ConnectionManager<TIDbConnection> : IConnectionManager, IDisposable where TIDbConnection : IDbConnection, new()
	{
		private readonly IDbConnection _iDbConnection;
		private IDbTransaction _iDbTransaction = null;

		IDbConnection IConnectionManager.Connection => _iDbConnection;
		IDbTransaction IConnectionManager.Transaction => _iDbTransaction;

		public ConnectionManager(string connectionString, Action setup = null)
		{
			setup?.Invoke();
			_iDbConnection = new TIDbConnection() { ConnectionString = connectionString };
			Open();
		}

		public void Open()
		{
			if (_iDbConnection.State != ConnectionState.Open)
				_iDbConnection.Open();
		}

		public void Close()
		{
			if (_iDbConnection.State != ConnectionState.Closed)
				_iDbConnection.Close();
		}

		public void Dispose()
		{
			Close();
			_iDbConnection.Dispose();
		}

		void IConnectionManager.BeginTransaction()
		{
			Open();
			_iDbTransaction = _iDbConnection.BeginTransaction();
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