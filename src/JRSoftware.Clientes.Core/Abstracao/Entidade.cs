using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public class Entidade
	{
		[Key, Required]
		public long Id { get; set; }
	}
}
