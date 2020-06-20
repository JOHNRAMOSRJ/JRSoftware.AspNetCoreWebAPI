using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class EnderecoDAL : BaseDAL
	{
		public EnderecoDAL() : base("Endereco", "Id", "ClienteId", "Logradouro", "Numero", "Complemento", "CidadeId")
		{

		}

		public Endereco Obter(IDataRecord dataRecord)
		{
			return new Endereco
			{
				Id = dataRecord.GetInt64(IndexOf("Id")),
				//ClienteId = dataRecord.GetInt64(IndexOf("ClienteId")),
				Logradouro = dataRecord.GetString(IndexOf("Logradouro")),
				Numero = dataRecord.GetString(IndexOf("Numero")),
				Complemento = dataRecord.GetString(IndexOf("Complemento")),
				Bairro = dataRecord.GetString(IndexOf("Bairro")),
				CidadeId = dataRecord.GetInt64(IndexOf("CidadeId")),
			};
		}

		public IEnumerable<Endereco> ObterPorClienteId(long clienteId)
		{
			var cmdSql = CmdSelect + " Where (ClienteId = @clienteId)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("clienteId", clienteId);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		protected override string CmdCreateTable => @"
Create Table Endereco (
	Id          Integer     Not Null Primary Key AutoIncrement,
	CidadeId    Integer     Not Null,
	ClienteId   Integer     Not Null,
	Logradouro  VarChar(30) Not Null,
	Numero      VarChar(30) Not Null,
	Complemento VarChar(30) Not Null,
	Bairro      VarChar(30) Not Null
)";
	}
}