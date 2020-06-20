using System;

namespace JRSoftware.Clientes.Core.Uteis
{
	public static class DateTimeExtension
	{
		public static int DiferencaEmAnos(this DateTime dataInicio, DateTime dataAtual)
		{
			var idade = dataAtual.Year - dataInicio.Year;

			if ((dataAtual.Month < dataInicio.Month) || ((dataAtual.Month == dataInicio.Month) && (dataAtual.Day < dataInicio.Day)))
				idade--;

			return idade;
		}
	}
}
