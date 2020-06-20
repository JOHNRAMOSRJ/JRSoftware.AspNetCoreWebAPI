using JRSoftware.Clientes.Core.Abstracao;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Endereco : Entidade
	{
		[Required, MaxLength(30)]
		public string Logradouro { get; set; }

		[Required, MaxLength(10)]
		public string Numero { get; set; }

		[Required, MaxLength(30)]
		public string Complemento { get; set; }

		[Required, MaxLength(30)]
		public string Bairro { get; set; }

		[Required]
		public Cidade Cidade { get; set; }

		[Required]
		public long CidadeId { get; internal set; }
	}
}