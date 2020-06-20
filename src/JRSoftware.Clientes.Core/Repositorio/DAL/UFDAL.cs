using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class UFDAL : BaseDAL
	{
		public UFDAL() : base("UF", "Id", "Sigla", "Nome")
		{

		}
	}
}