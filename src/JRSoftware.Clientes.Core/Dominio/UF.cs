using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Dominio
{
	public class UF : Entidade
	{
		public string Sigla { get; set; }
		public string Nome { get; set; }
	}
}