using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class CidadeDAL : BaseDAL
	{
		public CidadeDAL() : base("Cidade", "Id", "UFId", "Nome") { }

		public Cidade Obter(IDataRecord dataRecord)
		{
			return new Cidade
			{
				Id = dataRecord.GetInt64(IndexOf("Id")),
				UFId = dataRecord.GetInt64(IndexOf("UFId")),
				Nome = dataRecord.GetString(IndexOf("Nome")),
			};
		}

		public IEnumerable<Cidade> ObterPorNomeParcial(string nome)
		{
			var cmdSql = CmdSelect + " Where (nome Like @nome + '%')";
			var parametros = new Dictionary<string, object>();
			parametros.Add("nome", nome);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<Cidade> ObterPorNome(string nome)
		{
			var cmdSql = CmdSelect + " Where (nome = @nome)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("nome", nome);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<Cidade> ObterPorUFId(long ufId)
		{
			var cmdSql = CmdSelect + " Where (UFId = @ufId)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("ufId", ufId);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<Cidade> ObterPorId(long id)
		{
			var cmdSql = CmdSelect + " Where (Id = @id)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("id", id);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<Cidade> ObterPor(Cidade cidade)
		{
			var ufs = ObterPorId(cidade.Id);
			if (!ufs.Any())
				ufs = ObterPorNome(cidade.Nome);
			return ufs;
		}

		public IEnumerable<Cidade> ObterTodos()
		{
			return ExecuteReader(CmdSelect, null, dr => Obter(dr));
		}

		public void Incluir(Cidade cidade)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", cidade.Id);
			parametros.Add("UFId", cidade.UFId);
			parametros.Add("Nome", cidade.Nome);
			cidade.Id = ExecuteScalar(CmdInsert, parametros);
		}

		public void Alterar(Cidade cidade)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", cidade.Id);
			parametros.Add("UFId", cidade.UFId);
			parametros.Add("Nome", cidade.Nome);
			ExecuteScalar(CmdUpdate, parametros);
		}

		public void Excluir(Cidade cidade)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", cidade.Id);
			ExecuteScalar(CmdDelete, parametros);
		}

		protected override string CmdCreateTable => @"
Create Table Cidade (
	Id     Integer     Not Null Primary Key AutoIncrement,
	UFId   Integer     Not Null,
	Nome   VarChar(30) Not Null
)";
	}
}