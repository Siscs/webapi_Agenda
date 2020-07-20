using System.Collections.Generic;
using Siscs.Agenda.Business.Notificacoes;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface INotificador
    {
         bool TemNotificacao();
         List<Notificacao> ObterNotificacoes();
         void Handle(Notificacao notificacao);
    }
}