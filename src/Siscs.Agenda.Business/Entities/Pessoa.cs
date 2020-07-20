using Siscs.Agenda.Business.Enums;

namespace Siscs.Agenda.Business.Entities
{
    public class Pessoa : Entity
    {
        public TipoPessoa Tipo { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }
        
    }
}