using JRSoftware.Clientes.Core.Abstracao;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class UF : Entidade
	{
		[Required, MaxLength(2)]
		public string Sigla { get; set; }

		[Required, MaxLength(30)]
		public string Nome { get; set; }
	}
}