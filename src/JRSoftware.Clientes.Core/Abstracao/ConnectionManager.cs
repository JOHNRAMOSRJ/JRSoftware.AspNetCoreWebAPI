using System.Data;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public interface IConnectionManager
	{
		IDbConnection Connection { get; }
		IDbTransaction BeginTransaction();
		void EndTransaction(bool commit);
	}

	public class ConnectionManager<TIDbConnection> : IConnectionManager where TIDbConnection : IDbConnection, new()
	{
		private readonly IDbConnection _iDbConnection;
		private IDbTransaction _iDbTransaction = null;

		IDbConnection IConnectionManager.Connection => _iDbConnection;

		public ConnectionManager(string connectionString)
		{
			_iDbConnection = new TIDbConnection() { ConnectionString = connectionString };
		}

		public void Close()
		{
			_iDbConnection?.Close();
		}

		public void Open()
		{
			_iDbConnection?.Open();
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