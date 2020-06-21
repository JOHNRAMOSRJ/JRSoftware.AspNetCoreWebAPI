using System;

namespace JRSoftware.Clientes.Testes.Abstracao
{
	public class TesteBase
	{
		protected const long CPFValido = 96945230015L;

		protected long GerarCPFValido(long cpf)
		{
			return Convert.ToInt64(GerarCPFValido(cpf.ToString()));
		}

		protected string GerarCPFValido(string cpf)
		{
			var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

			cpf = cpf.Trim().Replace(".", "").Replace("-", "").PadLeft(9, '0');

			var tempCpf = cpf.Substring(0, 9);
			var soma = 0;

			for (var i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

			var resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			var digito = resto.ToString();
			tempCpf += digito;
			soma = 0;
			for (var i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			digito += resto.ToString();

			return cpf + digito;
		}

	}
}
