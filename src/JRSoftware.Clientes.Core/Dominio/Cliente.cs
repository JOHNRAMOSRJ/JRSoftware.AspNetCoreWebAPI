using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cliente : Entidade
	{
		[Required, MaxLength(30, ErrorMessage = "O Nome do Cliente precisa ter no máximo 30 caracteres")]
		public string Nome { get; set; }

		[Required]
		public long CPF { get; set; }

		[Required]
		public DateTime Nascimento { get; set; }

		[Required, JsonIgnore]
		private List<Endereco> ListaEnderecos { get; set; }

		public IEnumerable<Endereco> Enderecos => ListaEnderecos;

		public int Idade => DateTimeExtension.DiferencaEmAnos(Nascimento, DateTime.Today);

		public Cliente()
		{
			ListaEnderecos = new List<Endereco>();
		}

		public void AdicionarEnderecos(IEnumerable<Endereco> enderecos)
		{
			foreach (var endereco in enderecos)
				AdicionarEndereco(endereco);
		}

		public void AdicionarEndereco(Endereco endereco)
		{
			endereco.Cliente = this;
			ListaEnderecos.Add(endereco);
		}

		protected internal override IEnumerable<string> ObterValidacoes()
		{
			var validacoes = new List<string>();

			if (string.IsNullOrWhiteSpace(Nome))
				validacoes.Add("O Nome do Cliente precisa ser preenchido");
			else if (Nome.Length > 30)
				validacoes.Add("O Nome do Cliente precisa ter no máximo 30 caracteres");

			if (CPF <= 0)
				validacoes.Add("O CPF do Cliente precisa ser preenchido");
			else if (!DVExtension.CpfEhValido(CPF))
				validacoes.Add("O CPF do Cliente precisa ser válido");

			if (Nascimento == default)
				validacoes.Add("A Data de Nascimento do Cliente precisa ser preenchida");
			else if (Nascimento > DateTime.Today)
				validacoes.Add("A Data de Nascimento do Cliente precisa ser menor ou igual à data de hoje");

			if (!Enderecos.Any())
				validacoes.Add("O Cliente precisa ter ao menos um endereço válido");
			else
				validacoes.AddRange(Enderecos.SelectMany(e => e.ObterValidacoes()));

			return validacoes;
		}
	}
}