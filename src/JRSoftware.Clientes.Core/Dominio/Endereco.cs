using JRSoftware.Clientes.Core.Abstracao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Endereco : Entidade
	{
		[Required, JsonIgnore]
		public Cliente Cliente { get; internal set; }

		[Required]
		public Cidade Cidade { get; set; }

		[Required, MaxLength(50)]
		public string Logradouro { get; set; }

		[MaxLength(10)]
		public string Numero { get; set; }

		[MaxLength(20)]
		public string Complemento { get; set; }

		[Required, MaxLength(40)]
		public string Bairro { get; set; }

		#region // "Data Load"
		[JsonIgnore]
		public long ClienteId
		{
			get { return Cliente?.Id ?? 0L; }
			set { Cliente = new Cliente { Id = value }; }
		}
		[JsonIgnore]
		public long CidadeId
		{
			get { return Cidade?.Id ?? 0L; }
			set { Cidade = new Cidade { Id = value }; }
		}
		#endregion // "Data Load"

		protected internal override IEnumerable<string> ObterValidacoes()
		{
			var validacoes = new List<string>();

			if (Cliente == null)
				validacoes.Add("O Endereço precisa estar associado à um Cliente");

			if (Cidade == null)
				validacoes.Add("O Endereço precisa estar associado à uma Cidade");
			else
				validacoes.AddRange(Cidade.ObterValidacoes());

			if (string.IsNullOrWhiteSpace(Logradouro))
				validacoes.Add("O Logradouro do Endereço precisa ser preenchido");
			else if (Logradouro.Length > 50)
				validacoes.Add("O Logradouro do Endereço precisa ter no máximo 50 caracteres");

			if (string.IsNullOrWhiteSpace(Numero))
				validacoes.Add("O Número do Endereço precisa ser preenchido");
			else if (Numero.Length > 10)
				validacoes.Add("O Número do Endereço precisa ter no máximo 10 caracteres");

			if (string.IsNullOrWhiteSpace(Complemento))
				validacoes.Add("O Complemento do Endereço precisa ser preenchido");
			else if (Complemento.Length > 20)
				validacoes.Add("O Complemento do Endereço precisa ter no máximo 20 caracteres");

			if (string.IsNullOrWhiteSpace(Bairro))
				validacoes.Add("O Bairro do Endereço precisa ser preenchido");
			else if (Bairro.Length > 40)
				validacoes.Add("O Bairro do Endereço precisa ter no máximo 40 caracteres");

			return validacoes;
		}
	}
}