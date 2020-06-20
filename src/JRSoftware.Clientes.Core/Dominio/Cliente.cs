using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

		[Required]
		public List<Endereco> Enderecos { get; set; }

		public int Idade => DateTimeExtension.DiferencaEmAnos(Nascimento, DateTime.Today);

		public Cliente()
		{
			Enderecos = new List<Endereco>();
		}
	}
}