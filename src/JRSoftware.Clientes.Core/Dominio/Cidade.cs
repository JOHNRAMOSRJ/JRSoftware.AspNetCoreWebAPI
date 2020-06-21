using JRSoftware.Clientes.Core.Abstracao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cidade : Entidade
	{
		[Required]
		public UF UF { get; set; }

		[Required, MaxLength(40)]
		public string Nome { get; set; }

		#region // "Data Load"
		[JsonIgnore]
		public long UFId
		{
			get { return UF?.Id ?? 0L; }
			set { UF = new UF { Id = value }; }
		}
		#endregion // "Data Load"

		protected internal override IEnumerable<string> ObterValidacoes()
		{
			var validacoes = new List<string>();

			if (UF == null)
				validacoes.Add("A Cidade precisa estar associado à um Estado");
			else
				validacoes.AddRange(UF.ObterValidacoes());

			if (string.IsNullOrWhiteSpace(Nome))
				validacoes.Add("O Nome do Cliente precisa ser preenchido");
			else if (Nome.Length > 30)
				validacoes.Add("O Nome do Cliente precisa ter no máximo 30 caracteres");

			return validacoes;
		}
	}
}