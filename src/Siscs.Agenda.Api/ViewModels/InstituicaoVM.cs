using System.ComponentModel.DataAnnotations;

namespace Siscs.Agenda.Api.ViewModels
{
    public class InstituicaoVM
    {
        [Key]
        public long Id { get; set; }
        
        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [MaxLength(60, ErrorMessage="Deve conter até 60 caracteres")]
        [MinLength(3,ErrorMessage="Deve conter no mínimo 3 caracteres")]
        public string Nome { get; set; }
    }
}