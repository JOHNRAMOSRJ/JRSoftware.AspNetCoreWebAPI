using JRSoftware.Clientes.Core.Aplicacao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.WebAPI.Abstracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace JRSoftware.Clientes.WebAPI.Controllers
{
	public class ClientesController : ControllerAPI
	{
		private readonly ILogger<ClientesController> _logger;

		private ClienteService _clienteService;
		private ClienteService ClienteService => _clienteService ??= new ClienteService(ConnectionManager);
		public ClientesController(ILogger<ClientesController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<Cliente> Get()
		{
			return ClienteService.ObterTodos();
		}
	}
}