using FluentValidation;

namespace Siscs.Agenda.Business.Entities.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("O Campo {PropertyName} deve ser preenchido")
                .Length(3, 60).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
        
    }
}