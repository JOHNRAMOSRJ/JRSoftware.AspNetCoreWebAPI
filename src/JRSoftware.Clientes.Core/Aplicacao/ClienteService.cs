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

		private ClienteRepository _clienteRepository;
		private ClienteRepository ClienteRepository => _clienteRepository ??= new ClienteRepository { ConnectionManager = ConnectionManager };

		public ClienteService(IConnectionManager connectionManager) { ConnectionManager = connectionManager; }

		public IEnumerable<Cliente> ObterTodos()
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				return ClienteRepository.ObterTodos();
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

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				return ClienteRepository.ObterPorCPF(cpf);
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

		public IEnumerable<Cliente> ObterPorNome(string nome)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				return ClienteRepository.ObterPorNome(nome);
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
				ClienteRepository.Incluir(cliente);
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

		public void Alterar(Cliente cliente)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				ClienteRepository.Alterar(cliente);
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

		public void Excluir(Cliente cliente)
		{
			var commit = true;
			try
			{
				ConnectionManager.BeginTransaction();
				ClienteRepository.Excluir(cliente);
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
			ConnectionManager.Open();
			ClienteRepository.Setup();
			ConnectionManager.Close();
		}
	}
}