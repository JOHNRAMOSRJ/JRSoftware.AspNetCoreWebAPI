using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Aplicacao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Testes.Abstracao;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace JRSoftware.Clientes.Testes.Repositorio.DAL
{
	[TestClass]
	public class TestandoDAL : TesteBase
	{
		[TestMethod]
		public void QuandoIncluirClientes_DeveGerarOsIdentificadores()
		{
			using var connectionManager = ObterConnectionManager(true);
			var clienteService = new ClienteService(connectionManager);
			var cliente = CriarCliente(1);
			Assert.IsTrue(cliente.Id == 0);
			clienteService.Incluir(cliente);
			Assert.IsTrue(cliente.Id > 0);
		}

		[TestMethod]
		public void QuandoIncluir10ClientesCom2EnderecosCada_DeveRetornarOsMesmos10Clientes()
		{
			using var connectionManager = ObterConnectionManager(true);
			var clienteService = new ClienteService(connectionManager);
			for (int i = 1; i <= 10; i++)
			{
				var cliente = CriarCliente(i);
				clienteService.Incluir(cliente);
			}

			var clientes = clienteService.ObterTodos();
			Assert.IsTrue(clientes.Any());
			Assert.AreEqual(10, clientes.Count());
			Assert.AreEqual(20, clientes.SelectMany(c => c.Enderecos).Count());
		}

		#region // "Factories"

		private Cliente CriarCliente(int i)
		{
			var cliente = new Cliente() { CPF = GerarCPFValido(123456789 + i), Nascimento = new DateTime(2000, i, 15), Nome = $"Cliente {i}" };
			cliente.AdicionarEndereco(CriarEndereco(i, "Teresópolis", "RJ", "Rio de Janeiro"));
			cliente.AdicionarEndereco(CriarEndereco(i + 10, "São Paulo", "SP", "São Paulo"));
			return cliente;
		}

		private Endereco CriarEndereco(int i, string cidade, string uf, string nomeUf = null)
		{
			return new Endereco
			{
				Logradouro = "Rua dos Logradouros " + i,
				Numero = "1234",
				Complemento = "Apto 321",
				Bairro = "Nobre",
				Cidade = new Cidade
				{
					Nome = cidade,
					UF = new UF
					{
						Sigla = uf,
						Nome = nomeUf ?? uf
					}
				}
			};
		}

		public IConnectionManager ObterConnectionManager(bool forceCreate)
		{
			var databaseFile = Path.Combine(Path.GetTempPath(), "SQLiteAPI.db");

			if (forceCreate && File.Exists(databaseFile))
				File.Delete(databaseFile);

			var connectionString = $"Data Source={databaseFile}";
			var connectionManager = new ConnectionManager<SqliteConnection>(connectionString, () => SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_winsqlite3()));

			if (forceCreate)
				new ClienteService(connectionManager).Setup();

			return connectionManager;
		}

		#endregion // "Factories"
	}
}