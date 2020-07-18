using System.ComponentModel.DataAnnotations;

namespace Siscs.Agenda.Api.ViewModels
{
    public class UsuarioLoginVM
    {
        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage="O campo {0} está com formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [StringLength(20, ErrorMessage="O campo {0} precisar ter entre {2} e {1} caracteres", MinimumLength=6)]
        public string Senha { get; set; }

    }
}