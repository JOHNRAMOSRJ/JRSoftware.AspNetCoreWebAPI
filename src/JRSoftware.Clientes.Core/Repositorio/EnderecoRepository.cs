using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

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
			PreencherCidade(enderecos);
			cliente.AdicionarEnderecos(enderecos);
		}

		public IEnumerable<Endereco> ObterPorClienteId(long clienteId)
		{
			return EnderecoDAL.ObterPorClienteId(clienteId);
		}

		public void Incluir(IEnumerable<Endereco> enderecos)
		{
			foreach (var endereco in enderecos)
				Incluir(endereco);
		}

		public void Incluir(Endereco endereco)
		{
			endereco.Validar();
			PreencherCidade(endereco);
			EnderecoDAL.Incluir(endereco);
		}

		public void Incluir(Cidade cidade)
		{
			cidade.Validar();
			CidadeDAL.Incluir(cidade);
		}

		public void Incluir(UF uf)
		{
			uf.Validar();
			UFDAL.Incluir(uf);
		}

		public void Excluir(IEnumerable<Endereco> enderecos)
		{
			foreach (var endereco in enderecos)
				Excluir(endereco);
		}

		public void Excluir(Endereco endereco)
		{
			EnderecoDAL.Excluir(endereco);
		}

		public void Excluir(Cidade cidade)
		{
			CidadeDAL.Excluir(cidade);
		}

		public void Excluir(UF uf)
		{
			UFDAL.Excluir(uf);
		}

		private void PreencherCidade(IEnumerable<Endereco> enderecos)
		{
			foreach (var endereco in enderecos)
				PreencherCidade(endereco);
		}

		private void PreencherCidade(Endereco endereco)
		{
			var cidades = CidadeDAL.ObterPor(endereco.Cidade);
			if (cidades.Any())
				endereco.Cidade = cidades.FirstOrDefault();

			PreencherUF(endereco.Cidade);

			if (!cidades.Any())
				Incluir(endereco.Cidade);
		}

		private void PreencherUF(Cidade cidade)
		{
			var ufs = UFDAL.ObterPor(cidade.UF);
			if (ufs.Any())
				cidade.UF = ufs.FirstOrDefault();
			else
				Incluir(cidade.UF);
		}

		public void Setup()
		{
			UFDAL.Setup();
			CidadeDAL.Setup();
			EnderecoDAL.Setup();
		}
	}
}