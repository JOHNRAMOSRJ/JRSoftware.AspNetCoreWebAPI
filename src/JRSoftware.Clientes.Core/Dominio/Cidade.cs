using JRSoftware.Clientes.Core.Abstracao;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cidade : Entidade
	{
		[Required]
		public UF UF { get; set; }

		public long UFId
		{
			get { return UF?.Id ?? 0L; }
			set { UF = new UF { Id = value }; }
		}

		[Required, MaxLength(30)]
		public string Nome { get; set; }
	}
}