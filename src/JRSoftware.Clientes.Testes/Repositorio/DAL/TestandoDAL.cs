using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Aplicacao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace JRSoftware.Clientes.Testes.Repositorio.DAL
{
	[TestClass]
	public class TestandoDAL
	{
		[TestMethod]
		public void QuandoNaoTemBanco_DeveCriar()
		{
			var fileInfo = Path.Combine(Path.GetTempPath(), "SQLiteAPI.db");
			if (File.Exists(fileInfo))
				File.Delete(fileInfo);
			var connectionManager = new ConnectionManager<SqliteConnection>($"Data Source={fileInfo}", () => SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3()));

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

		[TestMethod]
		public void DeveIncluir10Clientes()
		{
			var clienteService = new ClienteService();
			for (int i = 0; i < 10; i++)
			{
				clienteService.Incluir(new Cliente() { CPF = i, Nascimento = new DateTime(2000, i + 1, 15), Nome = $"Cliente {i}" });
			}
		}
	}
}