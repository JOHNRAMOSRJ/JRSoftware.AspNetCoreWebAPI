using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using System.Collections.Generic;
using System.Linq;

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

		public IEnumerable<Cliente> ObterPorId(long id)
		{
			var clientes = ClienteDAL.ObterPorId(id);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public IEnumerable<Cliente> ObterPorNomeParcial(string nome)
		{
			var clientes = ClienteDAL.ObterPorNomeParcial(nome);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			var clientes = ClienteDAL.ObterPorCPF(cpf);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public IEnumerable<Cliente> ObterPor(Cliente cliente)
		{
			var clientes = ClienteDAL.ObterPor(cliente);
			EnderecoRepository.PreencherEnderecos(clientes);
			return clientes;
		}

		public void Incluir(Cliente cliente)
		{
			cliente.Validar(true);
			ClienteDAL.Incluir(cliente);
			EnderecoRepository.Incluir(cliente.Enderecos);
		}

		public void Alterar(Cliente cliente)
		{
			var clienteGravado = ObterPorId(cliente.Id).FirstOrDefault();
			EnderecoRepository.Excluir(clienteGravado?.Enderecos);
			ClienteDAL.Alterar(cliente);
			EnderecoRepository.Incluir(cliente.Enderecos);
		}

		public void Excluir(Cliente cliente)
		{
			var clienteGravado = ObterPorId(cliente.Id).FirstOrDefault();
			EnderecoRepository.Excluir(clienteGravado?.Enderecos);
			ClienteDAL.Excluir(clienteGravado);
		}

		public void Setup()
		{
			ClienteDAL.Setup();
			EnderecoRepository.Setup();
		}
	}
}