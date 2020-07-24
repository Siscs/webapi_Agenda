using System.Collections.Generic;

namespace Siscs.Agenda.Api.ViewModels
{
    public class TokenUsuarioVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimVM> Claims { get; set; }
    }
}