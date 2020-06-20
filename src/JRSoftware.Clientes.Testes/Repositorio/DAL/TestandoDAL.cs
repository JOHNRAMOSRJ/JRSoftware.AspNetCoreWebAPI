using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace JRSoftware.Clientes.Testes.Repositorio.DAL
{
	[TestClass]
	public class TestandoDAL
	{
		[TestMethod]
		public void QuandoNaoTemBanco_DeveCriar()
		{
			var fileInfo = Path.Combine(Path.GetTempPath(), "database.db");
			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3());
			var connectionManager = new ConnectionManager<SqliteConnection>($"Data Source={fileInfo}");

			var clienteDAL = new ClienteDAL() { ConnectionManager = connectionManager };
			var ufDAL = new UFDAL() { ConnectionManager = connectionManager };
			var cidadeDAL = new CidadeDAL() { ConnectionManager = connectionManager };
			var enderecoDAL = new EnderecoDAL() { ConnectionManager = connectionManager };

			Setup(clienteDAL, ufDAL, cidadeDAL, enderecoDAL);
		}

		private void Setup(params BaseDAL[] listaBaseDAL)
		{
			foreach (var baseDAL in listaBaseDAL)
				baseDAL.Setup();
		}
	}
}