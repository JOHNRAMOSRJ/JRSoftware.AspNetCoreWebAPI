using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using System;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Repositorio
{
	public class EnderecoRepository
	{
		public IConnectionManager ConnectionManager { get; set; }

		private EnderecoDAL _enderecoDAL;
		private CidadeDAL _cidadeDAL;
		private UFDAL _ufDAL;

		public EnderecoDAL EnderecoDAL => _enderecoDAL ?? (_enderecoDAL = new EnderecoDAL() { ConnectionManager = ConnectionManager });
		public CidadeDAL CidadeDAL => _cidadeDAL ?? (_cidadeDAL = new CidadeDAL() { ConnectionManager = ConnectionManager });
		public UFDAL UFDAL => _ufDAL ?? (_ufDAL = new UFDAL() { ConnectionManager = ConnectionManager });

		public void PreencherEnderecos(IEnumerable<Cliente> clientes)
		{
			foreach (var cliente in clientes)
				PreencherEnderecos(cliente);
		}

		private void PreencherEnderecos(Cliente cliente)
		{
			var enderecos = ObterPorClienteId(cliente.Id);
			cliente.Enderecos.AddRange(enderecos);
		}

		public IEnumerable<Endereco> ObterPorClienteId(long clienteId)
		{
			return EnderecoDAL.ObterPorClienteId(clienteId);
		}
	}
}
