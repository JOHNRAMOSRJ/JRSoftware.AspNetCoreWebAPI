using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JRSoftware.Clientes.Core.Abstracao
{

	public class BaseDAL
	{
		private readonly string Tabela;
		private readonly string[] Campos;
		internal IConnectionManager ConnectionManager { get; set; }

		protected string CmdSelect => $"Select {string.Join(", ", Campos)} From {Tabela} ";
		protected string CmdInsert => $"Insert Into {Tabela} ({string.Join(", ", Campos)}) Values ({string.Join(", ", Campos.Select(c => "@" + c))});";
		protected string CmdUpdate => $"Update {Tabela} Set {string.Join(", ", Campos.Select(c => $"{c} = @{c}"))} Where (Id = @id);";
		protected string CmdDelete => $"Delete From {Tabela} Where (Id = @id);";

		public BaseDAL(string tabela, params string[] campos)
		{
			Tabela = tabela;
			Campos = campos;
		}

		protected IEnumerable<TResult> ExecuteReader<TResult>(string cmdSQL, IDictionary<string, object> parameters, Func<IDataRecord, TResult> func)
		{
			var iDbCommand = ConnectionManager?.CreateCommandText(cmdSQL);
			iDbCommand?.AddParameters(parameters);
			return iDbCommand?.ExecuteReader(func);
		}
	}
}