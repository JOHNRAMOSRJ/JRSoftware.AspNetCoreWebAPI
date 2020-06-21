using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Repositorio;
using System;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public class BaseService
	{
		protected readonly IConnectionManager ConnectionManager;

		public BaseService(IConnectionManager connectionManager) { ConnectionManager = connectionManager; }

		protected void Transactional(Action acao)
		{
			TransactionalImpl(() => { acao.Invoke(); return true; });
		}

		protected TResult Transactional<TResult>(Func<TResult> acao)
		{
			return TransactionalImpl(acao);
		}

		private TResult TransactionalImpl<TResult>(Func<TResult> acao)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				return acao.Invoke();
			}
			catch (Exception)
			{
				commit = false;
				throw;
			}
			finally
			{
				ConnectionManager.EndTransaction(commit);
			}
		}

		public void Setup()
		{
			try
			{
				ConnectionManager.Open();
				var _clienteRepository = new ClienteRepository { ConnectionManager = ConnectionManager };
				_clienteRepository.Setup();
			}
			catch { }
			finally
			{
				ConnectionManager.Close();
				ConnectionManager.Open();
			}
		}
	}
}