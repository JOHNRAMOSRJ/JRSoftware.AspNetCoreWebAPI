using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class EnderecoDAL : BaseDAL
	{
		public EnderecoDAL() : base("Endereco", "Id", "ClienteId", "Logradouro", "Numero", "Complemento", "Bairro", "CidadeId")
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
				CidadeId = dataRecord.GetDateTime(IndexOf("CidadeId")),
			};
		}

		public IEnumerable<Endereco> ObterPorClienteId(long clienteId)
		{
			var cmdSql = CmdSelect + " Where (ClienteId = @clienteId)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("@clienteId", clienteId);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}
	}
}