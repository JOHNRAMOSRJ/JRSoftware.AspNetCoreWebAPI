using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JRSoftware.Clientes.Core.Abstracao
{
	public abstract class Entidade
	{
		[Key, Required]
		public long Id { get; set; }

		protected internal abstract IEnumerable<string> ObterValidacoes();

		public DomainValidation Validar(bool throwsException)
		{
			var validacoes = ObterValidacoes();
			var domainValidation = new DomainValidation(validacoes);
			return domainValidation.Validar(throwsException);
		}
	}
}