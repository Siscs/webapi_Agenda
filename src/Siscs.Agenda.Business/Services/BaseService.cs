using FluentValidation;
using FluentValidation.Results;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Business.Notificacoes;

namespace Siscs.Agenda.Business.Services
{
    public class BaseService
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificar(item.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<Tvalidation, Tentity>(Tvalidation validacao, Tentity entidade) 
            where Tvalidation : AbstractValidator<Tentity>
            where Tentity : Entity
        {

            ValidationResult validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);

            return false;

        }

    }
}