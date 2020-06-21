using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.IO;

namespace JRSoftware.Clientes.WebAPI.Abstracao
{
	[ApiController, Route("[controller]")]
	public class ControllerAPI : ControllerBase
	{
		private IConnectionManager _iConnectionManager;

		protected IConnectionManager ConnectionManager => _iConnectionManager ?? (_iConnectionManager = ObterConnectionManager());

		private IConnectionManager ObterConnectionManager()
		{
			var databaseFile = Path.Combine(Path.GetTempPath(), "SQLiteAPI.db");

			var connectionString = $"Data Source={databaseFile}";
			var connectionManager = new ConnectionManager<SqliteConnection>(connectionString, () => SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3()));

			new ClienteService(connectionManager).Setup();

			return connectionManager;
		}
	}
}
