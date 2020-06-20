using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Aplicacao
{
	public class ClienteService
	{
		private IConnectionManager ConnectionManager { get; set; }
		public ClienteService()
		{
			//ConnectionManager = new ConnectionManager<SqlConnection>("");
			ConnectionManager = new ConnectionManager<SqliteConnection>("");
		}

		public IEnumerable<Cliente> ObterTodos()
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				var clienteRepository = new ClienteRepository() { ConnectionManager = ConnectionManager };
				return clienteRepository.ObterTodos();
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
	}
}