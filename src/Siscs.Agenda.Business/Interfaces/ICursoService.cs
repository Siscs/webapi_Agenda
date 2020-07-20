using System.Threading.Tasks;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface ICursoService
    {
        Task<bool> Adicionar(Curso categoria);
        Task<bool> Alterar(Curso categoria);
        Task<bool> Excluir(long id);
    }
}