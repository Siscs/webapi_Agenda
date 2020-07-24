using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Siscs.Agenda.Business.Interfaces;

namespace Siscs.Agenda.Api.Extensions
{
    public class AspNetUsuario : IUsuario
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUsuario(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Nome => _accessor.HttpContext.User.Identity.Name;

        public long ObterId()
        {
            return Autenticado() ? long.Parse(_accessor.HttpContext.User.GetUserId()) : 0;
        }

        public string ObterEmail()
        {
            return Autenticado() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public bool Autenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool TemRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
    public static class AspNetUsuarioExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }

}