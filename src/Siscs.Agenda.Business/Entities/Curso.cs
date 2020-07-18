namespace Siscs.Agenda.Business.Entities
{
    public class Curso : Entity
    {
        public string Descricao { get; set; }
        public decimal Duracao { get; set; }
        public int Andamento { get; set; }
        public long InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    
    }
}