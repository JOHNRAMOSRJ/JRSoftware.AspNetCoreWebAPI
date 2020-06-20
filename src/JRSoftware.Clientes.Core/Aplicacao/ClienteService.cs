using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;

namespace JRSoftware.Clientes.Core.Aplicacao
{
	public class ClienteService
	{
		private IConnectionManager ConnectionManager { get; set; }
		public ClienteService()
		{
			//ConnectionManager = new ConnectionManager<SqlConnection>("");
			var fileInfo = Path.Combine(Path.GetTempPath(), "SQLiteAPI.db");
			ConnectionManager = new ConnectionManager<SqliteConnection>($"Data Source={fileInfo}", () => raw.SetProvider(new SQLite3Provider_winsqlite3()));
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

		public void Incluir(Cliente cliente)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				var clienteRepository = new ClienteRepository() { ConnectionManager = ConnectionManager };
				clienteRepository.Incluir(cliente);
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