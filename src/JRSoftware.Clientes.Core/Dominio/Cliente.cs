using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cliente : Entidade
	{
		[Required, MaxLength(30)]
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
	}
}