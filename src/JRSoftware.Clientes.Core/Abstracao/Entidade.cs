using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public abstract class Entidade
	{
		[Key, Required]
		public long Id { get; set; }

		protected internal abstract IEnumerable<string> ObterValidacoes();

		public void Validar()
		{
			var validacoes = ObterValidacoes();

			if (validacoes.Any())
			{
				var message = "Operação não realizada:\r\n - " + string.Join(";\r\n - ", validacoes) + ";\r\n\r\nVerifique as validações acima e tente novamente!";
				throw new ApplicationException(message);
			}
		}
	}
}
