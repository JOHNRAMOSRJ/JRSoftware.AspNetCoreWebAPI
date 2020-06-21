using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Repositorio
{
	public class ClienteRepository
	{
		public IConnectionManager ConnectionManager { get; set; }

		private ClienteDAL _clienteDAL;
		public ClienteDAL ClienteDAL => _clienteDAL ??= new ClienteDAL { ConnectionManager = ConnectionManager };

		private EnderecoRepository _enderecoRepository;
		public EnderecoRepository EnderecoRepository => _enderecoRepository ??= new EnderecoRepository { ConnectionManager = ConnectionManager };

		public IEnumerable<Cliente> ObterTodos()
		{
			var clientes = ClienteDAL.ObterTodos();
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public IEnumerable<Cliente> ObterPorNome(string nome)
		{
			var clientes = ClienteDAL.ObterPorNome(nome);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			var clientes = ClienteDAL.ObterPorCPF(cpf);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public void Incluir(Cliente cliente)
		{
			ClienteDAL.Incluir(cliente);
			EnderecoRepository.Incluir(cliente.Enderecos);
		}

		public void Alterar(Cliente cliente)
		{
			ClienteDAL.Alterar(cliente);
		}

		public void Excluir(Cliente cliente)
		{
			ClienteDAL.Excluir(cliente);
		}

		public void Setup()
		{
			ClienteDAL.Setup();
			EnderecoRepository.Setup();
		}
	}
}