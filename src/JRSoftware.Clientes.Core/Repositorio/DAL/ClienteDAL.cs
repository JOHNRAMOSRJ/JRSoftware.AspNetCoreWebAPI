using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	/// <summary>
	/// Create Table "Cliente" (
	///		"Id" BigInt Not Null,
	///		"Nome" VarChar(30) Not Null,
	///		"CPF" BigInt Not Null,
	///		"Nascimento" Date  Not Null
	///	);
	/// </summary>
	public class ClienteDAL : BaseDAL
	{
		public ClienteDAL() : base("Cliente", "Id", "Nome", "CPF", "Nascimento") { }

		public Cliente Obter(IDataRecord dataRecord)
		{
			return new Cliente
			{
				Id = dataRecord.GetInt64(IndexOf("Id")),
				Nome = dataRecord.GetString(IndexOf("Nome")),
				CPF = dataRecord.GetInt64(IndexOf("CPF")),
				Nascimento = dataRecord.GetDateTime(IndexOf("Nascimento"))
			};
		}

		public IEnumerable<Cliente> ObterTodos()
		{
			return ExecuteReader(CmdSelect, null, dr => Obter(dr));
		}

		public IEnumerable<Cliente> ObterPorNome(string nome)
		{
			var cmdSql = CmdSelect + " Where (Nome Like @nome + '%')";
			var parametros = new Dictionary<string, object>();
			parametros.Add("@nome", nome);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<Cliente> ObterPorCPF(long cpf)
		{
			var cmdSql = CmdSelect + " Where (CPF = @cpf)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("@cpf", cpf);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public void Incluir(Cliente cliente)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", cliente.Id);
			parametros.Add("Nome", cliente.Nome);
			parametros.Add("CPF", cliente.CPF);
			parametros.Add("Nascimento", cliente.Nascimento);
			cliente.Id = ExecuteScalar(CmdInsert, parametros);
		}

		public void Alterar(Cliente cliente)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", cliente.Id);
			parametros.Add("Nome", cliente.Nome);
			parametros.Add("CPF", cliente.CPF);
			parametros.Add("Nascimento", cliente.Nascimento);
			cliente.Id = ExecuteScalar(CmdUpdate, parametros);
		}

		public void Excluir(Cliente cliente)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("@Id", cliente.Id);
			ExecuteScalar(CmdDelete, parametros);
		}


		protected override string CmdCreateTable => @"
Create Table Cliente (
	Id         Integer     Not Null Primary Key AutoIncrement,
	Nome       VarChar(30) Not Null,
	CPF        BigInt      Not Null,
	Nascimento Date        Not Null
)";
	}
}