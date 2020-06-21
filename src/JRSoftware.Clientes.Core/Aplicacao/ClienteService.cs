using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Aplicacao
{
	public class ClienteService : BaseService
	{
		private ClienteRepository _clienteRepository;
		private ClienteRepository ClienteRepository => _clienteRepository ?? (_clienteRepository = new ClienteRepository { ConnectionManager = ConnectionManager });

		public ClienteService(IConnectionManager connectionManager) : base(connectionManager) { }

		public IEnumerable<Cliente> ObterTodos()
		{
			return Transactional(() => ClienteRepository.ObterTodos());
		}

		public IEnumerable<Cliente> ObterPorId(long id)
		{
			return Transactional(() => ClienteRepository.ObterPorId(id));
		}

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			return Transactional(() => ClienteRepository.ObterPorCPF(cpf));
		}

		public IEnumerable<Cliente> ObterPorCPF(string strCPF)
		{
			return Transactional(() => ClienteRepository.ObterPorCPF(strCPF));
		}

		public IEnumerable<Cliente> ObterPorNomeParcial(string nome)
		{
			return Transactional(() => ClienteRepository.ObterPorNomeParcial(nome));
		}

		public IEnumerable<Cliente> ObterPorNome(string nome)
		{
			return Transactional(() => ClienteRepository.ObterPorNome(nome));
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
	}
}