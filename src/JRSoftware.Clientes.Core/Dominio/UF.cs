using JRSoftware.Clientes.Core.Abstracao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class UF : Entidade
	{
		[MaxLength(2)]
		public string Sigla { get; set; }

		[Required, MaxLength(40)]
		public string Nome { get; set; }

		protected internal override IEnumerable<string> ObterValidacoes()
		{
			var validacoes = new List<string>();

			if (string.IsNullOrWhiteSpace(Nome))
				validacoes.Add("O Nome do Estado precisa ser preenchido");
			else if (Nome.Length > 40)
				validacoes.Add("O Nome do Estado precisa ter no máximo 40 caracteres");

			return validacoes;
		}
	}
}