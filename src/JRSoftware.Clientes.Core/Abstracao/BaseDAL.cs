using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public abstract class BaseDAL
	{
		private readonly string Tabela;
		private readonly string[] Campos;
		private readonly string[] CamposInsertUpdate;

		public IConnectionManager ConnectionManager { get; set; }

		protected string CmdSelect => $"Select {string.Join(", ", Campos)} From {Tabela} ";
		protected string CmdInsert => $"Insert Into {Tabela} ({string.Join(", ", CamposInsertUpdate)}) Values ({string.Join(", ", CamposInsertUpdate.Select(c => "@" + c))}); Select last_insert_rowid();";
		protected string CmdUpdate => $"Update {Tabela} Set {string.Join(", ", CamposInsertUpdate.Select(c => $"{c} = @{c}"))} Where (Id = @id);";
		protected string CmdDelete => $"Delete From {Tabela} Where (Id = @id);";

		public BaseDAL(string tabela, params string[] campos)
		{
			Tabela = tabela;
			Campos = campos;
			CamposInsertUpdate = Campos.Where(c => c.ToUpper() != "ID").ToArray();
		}

		protected IEnumerable<TResult> ExecuteReader<TResult>(string cmdSQL, IDictionary<string, object> parameters, Func<IDataRecord, TResult> func)
		{
			var iDbCommand = ConnectionManager?.CreateCommandText(cmdSQL, ConnectionManager.Transaction);
			iDbCommand?.AddParameters(parameters);
			return iDbCommand?.ExecuteReader(func);
		}

		protected long ExecuteScalar(string cmdSQL, IDictionary<string, object> parameters)
		{
			var iDbCommand = ConnectionManager?.CreateCommandText(cmdSQL, ConnectionManager.Transaction);
			iDbCommand?.AddParameters(parameters);
			var retorno = iDbCommand?.ExecuteScalar();
			return Convert.ToInt64(retorno);
		}

		protected int? ExecuteNonQuery(string cmdSQL, IDictionary<string, object> parameters)
		{
			var iDbCommand = ConnectionManager?.CreateCommandText(cmdSQL, ConnectionManager.Transaction);
			iDbCommand?.AddParameters(parameters);
			return iDbCommand?.ExecuteNonQuery();
		}
		protected int IndexOf(string campo) => IndexOf(Campos, campo);

		protected static int IndexOf(IEnumerable<string> strings, string key)
		{
			var array = strings.Select(c => c.ToUpper()).ToArray();
			var item = key.ToUpper();
			var index = 0;
			while ((index < array.Length) && (array[index] != key))
				index++;
			return ((index < array.Length) && (array[index] == key)) ? index : -1;
		}

		protected abstract string CmdCreateTable { get; }

		public void Setup()
		{
			ExecuteNonQuery(CmdCreateTable, null);
		}
	}
}