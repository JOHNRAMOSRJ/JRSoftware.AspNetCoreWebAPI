using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class CidadeDAL: BaseDAL
	{
		public CidadeDAL() : base("Cidade", "Id", "UFId", "Nome")
		{

		}
	}
}