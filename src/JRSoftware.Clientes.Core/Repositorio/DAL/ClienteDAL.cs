using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class ClienteDAL : BaseDAL
	{
		public ClienteDAL() : base("Cliente", "Id", "Nome", "CPF", "Nascimento")
		{

		}

		public IEnumerable<Cliente> ObterTodos()
		{
			return ExecuteReader(CmdSelect, null, Obter);
		}

		public Cliente Obter(IDataRecord dataRecord)
		{
			return new Cliente
			{
				Id = dataRecord.GetInt64(0),
				Nome = dataRecord.GetString(1),
				CPF = dataRecord.GetInt64(2),
				Nascimento = dataRecord.GetDateTime(3),
			};
		}
	}
}