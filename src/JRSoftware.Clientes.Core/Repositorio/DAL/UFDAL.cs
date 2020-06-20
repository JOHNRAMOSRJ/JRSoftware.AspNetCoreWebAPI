using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;

namespace JRSoftware.Clientes.Core.Repositorio.DAL
{
	public class UFDAL : BaseDAL
	{
		public UFDAL() : base("UF", "Id", "Sigla", "Nome")
		{

		}

		protected override string CmdCreateTable => @"
Create Table UF (
	Id     Integer     Not Null Primary Key AutoIncrement,
	Sigla  VarChar(2)  Not Null,
	Nome   VarChar(30) Not Null
)";
	}
}