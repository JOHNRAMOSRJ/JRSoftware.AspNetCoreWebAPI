using JRSoftware.Clientes.Core.Abstracao;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cidade : Entidade
	{
		[Required]
		public UF UF { get; set; }
		
		[Required, MaxLength(30)]
		public string Nome { get; set; }
	}
}