using JRSoftware.Clientes.Core.Aplicacao;
using JRSoftware.Clientes.Core.Dominio;
using JRSoftware.Clientes.WebAPI.Abstracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace JRSoftware.Clientes.WebAPI.Controllers
{
	[ApiController, Route("[controller]")]
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

		[HttpGet, Route("{id}")]
		public Cliente Get(long id)
		{
			return ClienteService.ObterPorId(id).FirstOrDefault();
		}

		[HttpGet, Route("cpf/{cpf}")]
		public IEnumerable<Cliente> Get(string cpf)
		{
			return ClienteService.ObterPorCPF(cpf);
		}
		
		[HttpGet, Route("nome/{nome}/{parcial}")]
		public IEnumerable<Cliente> Get(string nome, bool parcial = false)
		{
			return parcial ? ClienteService.ObterPorNomeParcial(nome) : ClienteService.ObterPorNome(nome);
		}




	}
}