using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Aplicacao
{
	public class ClienteService : BaseService
	{
		private ClienteRepository _clienteRepository;
		private ClienteRepository ClienteRepository => _clienteRepository ??= new ClienteRepository { ConnectionManager = ConnectionManager };

		public ClienteService(IConnectionManager connectionManager) : base(connectionManager) { }

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
	}
}