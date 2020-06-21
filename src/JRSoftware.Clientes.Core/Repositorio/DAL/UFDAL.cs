using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class UFDAL : BaseDAL
	{
		public UFDAL() : base("UF", "Id", "Sigla", "Nome") { }

		public UF Obter(IDataRecord dataRecord)
		{
			return new UF
			{
				Id = dataRecord.GetInt64(IndexOf("Id")),
				Sigla = dataRecord.GetString(IndexOf("Sigla")),
				Nome = dataRecord.GetString(IndexOf("Nome")),
			};
		}

		public IEnumerable<UF> ObterPorNomeParcial(string nome)
		{
			var cmdSql = CmdSelect + " Where (nome Like @nome + '%')";
			var parametros = new Dictionary<string, object>();
			parametros.Add("nome", nome);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<UF> ObterPorNome(string nome)
		{
			var cmdSql = CmdSelect + " Where (nome = @nome)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("nome", nome);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<UF> ObterPorSigla(string sigla)
		{
			var cmdSql = CmdSelect + " Where (sigla = @sigla)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("sigla", sigla);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<UF> ObterPorId(long id)
		{
			var cmdSql = CmdSelect + " Where (Id = @id)";
			var parametros = new Dictionary<string, object>();
			parametros.Add("id", id);
			return ExecuteReader(cmdSql, parametros, dr => Obter(dr));
		}

		public IEnumerable<UF> ObterPor(UF uf)
		{
			var ufs = ObterPorId(uf.Id);
			if (!ufs.Any())
				ufs = ObterPorSigla(uf.Sigla);
			if (!ufs.Any())
				ufs = ObterPorNome(uf.Nome);
			return ufs;
		}

		public IEnumerable<UF> ObterTodos()
		{
			return ExecuteReader(CmdSelect, null, dr => Obter(dr));
		}

		public void Incluir(UF uf)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", uf.Id);
			parametros.Add("Sigla", uf.Sigla);
			parametros.Add("Nome", uf.Nome);
			uf.Id = ExecuteScalar(CmdInsert, parametros);
		}

		public void Alterar(UF uf)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", uf.Id);
			parametros.Add("Sigla", uf.Sigla);
			parametros.Add("Nome", uf.Nome);
			ExecuteScalar(CmdUpdate, parametros);
		}

		public void Excluir(UF uf)
		{
			var parametros = new Dictionary<string, object>();
			parametros.Add("Id", uf.Id);
			ExecuteScalar(CmdDelete, parametros);
		}

		protected override string CmdCreateTable => @"
Create Table UF (
	Id     Integer     Not Null Primary Key AutoIncrement,
	Sigla  VarChar(2)  Not Null,
	Nome   VarChar(30) Not Null
)";
	}
}