using System.Linq;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public class BaseDAL
	{
		private string Tabela { get; set; }
		private string[] Campos { get; set; }

		public BaseDAL(string tabela, params string[] campos)
		{
			Tabela = tabela;
			Campos = campos;
		}

		public string CmdSelect => $"Select {string.Join(", ", Campos)} From {Tabela} ";
		public string CmdInsert => $"Insert Into {Tabela} ({string.Join(", ", Campos)}) Values ({string.Join(", ", Campos.Select(c => "@" + c))});";
		public string CmdUpdate => $"Update {Tabela} Set {string.Join(", ", Campos.Select(c => $"{c} = @{c}"))} Where (Id = @id);";
		public string CmdDelete => $"Delete From {Tabela} Where (Id = @id);";
	}
}
