using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class EnderecoDAL : BaseDAL
	{
		public EnderecoDAL() : base("Endereco", "Id", "ClienteId", "Logradouro", "Numero", "Complemento", "Bairro", "CidadeId")
		{

		}
	}
}