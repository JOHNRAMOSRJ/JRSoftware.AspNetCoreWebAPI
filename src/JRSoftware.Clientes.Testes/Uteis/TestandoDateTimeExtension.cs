using JRSoftware.Clientes.Core.Uteis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JRSoftware.Clientes.Testes.Uteis
{
	[TestClass]
	public class TestandoDateTimeExtension
	{
		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre01Jan2020e01Jan2019_DeveRetornar1Ano()
		{
			var dataInicio = new DateTime(2019, 01, 01);
			var dataAtual = new DateTime(2020, 01, 01);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(1, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre31Dez2019e01Jan2019_DeveRetornarDiferencaZero()
		{
			var dataInicio = new DateTime(2019, 01, 01);
			var dataAtual = new DateTime(2019, 12, 31);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(0, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e14Fev2020_DeveRetornar19Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 02, 14);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(19, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e15Fev2020_DeveRetornar19Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 02, 15);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(19, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e16Fev2020_DeveRetornar19Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 02, 16);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(19, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e14Mar2020_DeveRetornar19Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 03, 14);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(19, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e15Mar2020_DeveRetornar20Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 03, 15);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(20, diferenca);
		}

		[TestMethod]
		public void QuandoSolicitaDiferencaEmAnosEntre15Mar2000e16Mar2020_DeveRetornar20Anos()
		{
			var dataInicio = new DateTime(2000, 03, 15);
			var dataAtual = new DateTime(2020, 03, 16);
			var diferenca = DateTimeExtension.DiferencaEmAnos(dataInicio, dataAtual);
			Assert.AreEqual(20, diferenca);
		}
	}
}