using JRSoftware.Clientes.Core.Abstracao;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Endereco : Entidade
	{
		[JsonIgnore]
		public Cliente Cliente { get; internal set; }
		
		[JsonIgnore]
		public long ClienteId
		{
			get { return Cliente?.Id ?? 0L; }
			set { Cliente = new Cliente { Id = value }; }
		}

		[Required]
		public Cidade Cidade { get; set; }

		[JsonIgnore]
		public long CidadeId
		{
			get { return Cidade?.Id ?? 0L; }
			set { Cidade = new Cidade { Id = value }; }
		}

		[Required, MaxLength(30)]
		public string Logradouro { get; set; }

		[Required, MaxLength(10)]
		public string Numero { get; set; }

		[Required, MaxLength(30)]
		public string Complemento { get; set; }

		[Required, MaxLength(30)]
		public string Bairro { get; set; }

	}
}