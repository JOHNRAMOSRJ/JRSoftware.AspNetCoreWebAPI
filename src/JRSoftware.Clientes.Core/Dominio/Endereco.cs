using JRSoftware.Clientes.Core.Abstracao;
using System;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Endereco : Entidade
	{
		public string Logradouro { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public Cidade Cidade { get; set; }
		public DateTime CidadeId { get; internal set; }
	}
}