using JRSoftware.Clientes.Core.Abstracao;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class CidadeDAL: BaseDAL
	{
		public CidadeDAL() : base("Cidade", "Id", "UFId", "Nome")
		{
		}

		protected override string CmdCreateTable => @"
Create Table Cidade (
	Id     Integer     Not Null Primary Key AutoIncrement,
	UFId   Integer     Not Null,
	Nome   VarChar(30) Not Null
)";
	}
}