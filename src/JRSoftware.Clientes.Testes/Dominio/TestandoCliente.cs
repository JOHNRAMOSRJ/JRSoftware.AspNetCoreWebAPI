using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Testes.Abstracao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace JRSoftware.Clientes.Testes.Dominio
{
	[TestClass]
	public class TestandoCliente : TesteBase
	{
		private Endereco ObterEnderecoValido()
		{
			return new Endereco
			{
				Logradouro = "Rua dos Logradouros",
				Numero = "1234",
				Complemento = "Apto 321",
				Bairro = "Nobre",
				Cidade = new Cidade
				{
					Nome = "Cidade",
					UF = new UF
					{
						Sigla = "RJ",
						Nome = "RJ"
					}
				}
			};
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteComTodosOsCamposObrigatorios_OClienteDeveSerValido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nascimento = new DateTime(2000, 1, 1), Nome = $"Cliente da Silva" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsTrue(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.AreEqual(DateTime.Today.Year - 2000, cliente.Idade);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteComTodosOsCamposObrigatoriosSemEndereco_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nascimento = new DateTime(2000, 1, 15), Nome = $"Cliente da Silva" };

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("Endereço")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteSemNome_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nascimento = new DateTime(2000, 1, 15), Nome = " " };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("Nome")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteComNomeMuitoGrande_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nascimento = new DateTime(2000, 1, 15), Nome = "Cliente Jose Mario Santos da Silva Moreira Pacheco" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("Nome")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteSemDataNascimento_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nome = "Cliente" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("Nascimento")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteComDataNascimentoProFuturo_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido, Nascimento = DateTime.Today.AddDays(1), Nome = "Cliente" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("Nascimento")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteSemCPF_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { Nascimento = new DateTime(2000, 1, 15), Nome = "Cliente" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("CPF")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}

		[TestMethod]
		public void QuandoInstanciaUmClienteComCPFInvalido_OClienteDeveSerInvalido()
		{
			var cliente = new Cliente { CPF = CPFValido + 5, Nascimento = new DateTime(2000, 1, 15), Nome = "Cliente" };
			cliente.AdicionarEndereco(ObterEnderecoValido());

			var domainValidation = cliente.Validar(false);

			Assert.IsFalse(domainValidation.EhValido, domainValidation.Mensagem);
			Assert.IsTrue(domainValidation.Validacoes.Any(m => m.Contains("CPF")), domainValidation.Mensagem);
			Assert.ThrowsException<ApplicationException>(() => domainValidation.Validar(true), domainValidation.Mensagem);
		}
	}
}
