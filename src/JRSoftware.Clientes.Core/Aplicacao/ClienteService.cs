using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio;
using System;
using System.Collections.Generic;

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
			return Transactional(() => ClienteRepository.ObterTodos());
		}

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			return Transactional(() => ClienteRepository.ObterPorCPF(cpf));
		}

		public IEnumerable<Cliente> ObterPorNome(string nome)
		{
			return Transactional(() => ClienteRepository.ObterPorNomeParcial(nome));
		}

		public void Incluir(Cliente cliente)
		{
			Transactional(() => ClienteRepository.Incluir(cliente));
		}

		public void Alterar(Cliente cliente)
		{
			Transactional(() => ClienteRepository.Alterar(cliente));
		}

		public void Excluir(Cliente cliente)
		{
			Transactional(() => ClienteRepository.Excluir(cliente));
		}

		public void Transactional(Action acao)
		{
			Transactional(() => acao.Invoke());
		}

		public TResult Transactional<TResult>(Func<TResult> acao)
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
				ClienteRepository.Setup();
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