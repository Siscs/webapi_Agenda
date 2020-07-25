using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Business.Notificacoes;

namespace Siscs.Agenda.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        private readonly IUsuario _usuario;

        protected long UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public bool UsuarioAutenticado { get; set; }

        public MainController(INotificador notificador, IUsuario usuario)
        {
            _notificador = notificador;
            _usuario = usuario;

            // if(_usuario.Autenticado())
            // {
            //     UsuarioAutenticado = true;
            //     UsuarioId = _usuario.ObterId();
            //     UsuarioNome = _usuario.Nome;
            // }
        }

        protected bool TemNotificacoes()
        {
            return _notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(!TemNotificacoes())
            {
                return Ok(new 
                {
                    success = true, 
                    data = result
                });
            }

            return BadRequest(new 
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(m => m.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) 
                NotificarErrosModelState(modelState);

            return CustomResponse();
        }

        private void NotificarErrosModelState(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var mensagemErro = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(mensagemErro);
            }

        }

        protected void NotificarErro(string mensagemErro)
        {
            _notificador.Handle(new Notificacao(mensagemErro));
        }
    }
}