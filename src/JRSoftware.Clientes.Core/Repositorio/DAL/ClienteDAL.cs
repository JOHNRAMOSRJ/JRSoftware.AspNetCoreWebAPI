using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class ClienteDAL : BaseDAL
	{
		public ClienteDAL() : base("Cliente", "Id", "Nome", "CPF", "Nascimento")
		{

		}
	}
}
