using System.ComponentModel.DataAnnotations;

namespace Siscs.Agenda.Api.ViewModels
{
    public class CursoVM
    {
        [Key]
        public long Id { get; set; }  

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [MaxLength(60, ErrorMessage="Deve conter até 60 caracteres")]
        [MinLength(3,ErrorMessage="Deve conter no mínimo 3 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [Range(1, 100000, ErrorMessage="Valor de 0 a 100%")]
        public decimal Duracao { get; set; }

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [Range(0, 100, ErrorMessage="Valor de 0 a 100%")]
        public int Andamento { get; set; }

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage="informe uma id válida")]
        public long LocalId { get; set; }
        public InstituicaoVM Instituicao { get; set; }

        [Required(ErrorMessage="O campo {0} é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage="informe uma id válida")]
        public long CategoriaId { get; set; }
        public CategoriaVM Categoria { get; set; }
    
    }
}