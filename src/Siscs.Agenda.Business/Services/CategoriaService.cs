using System.Linq;
using System.Threading.Tasks;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Entities.Validations;
using Siscs.Agenda.Business.Interfaces;

namespace Siscs.Agenda.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICursoRepository _cursoRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                                INotificador notificador, 
                                ICursoRepository cursoRepository) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<bool> Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria) ) return false;

            if(_categoriaRepository.Pesquisar(c => c.Descricao == categoria.Descricao).Result.Any())
            {
                Notificar("Já existe uma categoria com a descrição informada.");
                return false;
            }
            
            await _categoriaRepository.Adicionar(categoria);

            return true;

        }

        public async Task<bool> Alterar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria) ) return false;

            if(_categoriaRepository.Pesquisar(c => c.Descricao == categoria.Descricao && c.Id != categoria.Id).Result.Any())
            {
                Notificar("Já existe uma categoria com a descrição informada.");
                return false;
            }
            
            await _categoriaRepository.Alterar(categoria);

            return true;
        }

        public async Task<bool> Excluir(long id)
        {
            if(_cursoRepository.Pesquisar(c => c.CategoriaId == id ).Result.Any())
            {
                Notificar("Existem cursos para esta categoria.");
                return false;
            }

            await _categoriaRepository.Excluir(id);

            return true;
        }
    }
}