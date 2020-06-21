using JRSoftware.Clientes.Core.Uteis;
using JRSoftware.Clientes.Testes.Abstracao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JRSoftware.Clientes.Testes.Uteis
{
	[TestClass]
	public class TestandoDVExtension: TesteBase
	{
		[TestMethod]
		public void QuandoTestaUmCPFZerado_DeveRetornarFalso()
		{
			var resultado = DVExtension.CpfEhValido(0);
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		public void QuandoTestaUmCPFComZeros_DeveRetornarFalso()
		{
			var resultado = DVExtension.CpfEhValido("00000000000");
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		public void QuandoTestaUmCPFInvalido_DeveRetornarFalso()
		{
			var resultado = DVExtension.CpfEhValido(12345678901);
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		public void QuandoTestaUmCPFValido_DeveRetornarVerdadeiro()
		{
			var resultado = DVExtension.CpfEhValido(CPFValido);
			Assert.IsTrue(resultado);
		}
	}
}