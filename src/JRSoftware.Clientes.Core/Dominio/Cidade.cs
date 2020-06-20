using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class Cidade : Entidade
	{
		public UF UF { get; set; }
		public string Nome { get; set; }
	}
}