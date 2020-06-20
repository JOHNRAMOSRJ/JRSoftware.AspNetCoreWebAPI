using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Uteis;
using System;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cliente : Entidade
	{
		public string Nome { get; set; }
		public long CPF { get; set; }
		public DateTime Nascimento { get; set; }
		private List<Endereco> _enderecos { get; set; }

		private IEnumerable<Endereco> Enderecos => _enderecos;
		public int Idade => DateTimeExtension.DiferencaEmAnos(Nascimento, DateTime.Today);

		public Cliente()
		{
			_enderecos = new List<Endereco>();
		}
	}
}