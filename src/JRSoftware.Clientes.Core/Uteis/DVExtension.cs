using System;
using System.Collections.Generic;
using System.Text;

namespace JRSoftware.Clientes.Core.Uteis
{
	public class DVExtension
	{
		public static bool CpfEhValido(long cpf)
		{
			return CpfEhValido(cpf.ToString().PadLeft(11, '0'));
		}

		public static bool CpfEhValido(string cpf)
		{
			var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

			cpf = cpf.Trim().Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;

			for (var j = 0; j < 10; j++)
				if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
					return false;

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

			return cpf.EndsWith(digito);
		}
	}
}
