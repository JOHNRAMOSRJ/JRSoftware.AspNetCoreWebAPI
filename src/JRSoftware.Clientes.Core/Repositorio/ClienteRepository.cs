using JRSoftware.Clientes.Core.Abstracao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.Core.Repositorio.DAL;
using System;
using System.Collections.Generic;

namespace JRSoftware.Clientes.Core.Repositorio
{
	public class ClienteRepository
	{
		public IConnectionManager ConnectionManager { get; set; }
		public ClienteDAL ClienteDAL => new ClienteDAL() { ConnectionManager = ConnectionManager };

		public IEnumerable<Cliente> ObterTodos()
		{
			return ClienteDAL.ObterTodos();
		}
	}
}
