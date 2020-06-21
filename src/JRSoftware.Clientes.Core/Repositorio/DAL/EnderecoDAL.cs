using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class EnderecoDAL : BaseDAL
	{
		public EnderecoDAL() : base("Endereco", "Id", "CidadeId", "ClienteId", "Logradouro", "Numero", "Complemento", "Bairro") { }

		public Endereco Obter(IDataRecord dataRecord)
		{
			return new Endereco
			{
				Id = dataRecord.GetInt64(IndexOf("Id")),
				CidadeId = dataRecord.GetInt64(IndexOf("CidadeId")),
				ClienteId = dataRecord.GetInt64(IndexOf("ClienteId")),
				Logradouro = dataRecord.GetString(IndexOf("Logradouro")),
				Numero = dataRecord.GetString(IndexOf("Numero")),
				Complemento = dataRecord.GetString(IndexOf("Complemento")),
				Bairro = dataRecord.GetString(IndexOf("Bairro")),
			};
		}

		public IEnumerable<Endereco> ObterPorClienteId(long clienteId)
		{
			var cmdSql = CmdSelect + " Where (ClienteId = @clienteId)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("clienteId", clienteId);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public void Incluir(Endereco endereco)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", endereco.Id);
			parametros.Add("CidadeId", endereco.CidadeId);
			parametros.Add("ClienteId", endereco.ClienteId);
			parametros.Add("Logradouro", endereco.Logradouro);
			parametros.Add("Numero", endereco.Numero);
			parametros.Add("Complemento", endereco.Complemento);
			parametros.Add("Bairro", endereco.Bairro);
			endereco.Id = ExecuteScalar(CmdInsert, parametros);
		}

		public void Alterar(Endereco endereco)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", endereco.Id);
			parametros.Add("CidadeId", endereco.CidadeId);
			parametros.Add("ClienteId", endereco.ClienteId);
			parametros.Add("Logradouro", endereco.Logradouro);
			parametros.Add("Numero", endereco.Numero);
			parametros.Add("Complemento", endereco.Complemento);
			parametros.Add("Bairro", endereco.Bairro);
			ExecuteScalar(CmdUpdate, parametros);
		}

		public void Excluir(Endereco endereco)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", endereco.Id);
			ExecuteScalar(CmdDelete, parametros);
		}

		protected override string CmdCreateTable => @"
Create Table Endereco (
	Id          Integer     Not Null Primary Key AutoIncrement,
	CidadeId    Integer     Not Null,
	ClienteId   Integer     Not Null,
	Logradouro  VarChar(50) Not Null,
	Numero      VarChar(10) Not Null,
	Complemento VarChar(20) Not Null,
	Bairro      VarChar(40) Not Null
)";
	}
}