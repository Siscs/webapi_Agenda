using System.Collections.Generic;
using System.Security.Claims;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface IUsuario
    {
         string Nome { get; }

         long ObterId();

         string ObterEmail();

         bool Autenticado();
         bool TemRole(string role);
         IEnumerable<Claim> ObterClaims();

    }
}