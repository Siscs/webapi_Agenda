using System.Threading.Tasks;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface ICategoriaService
    {
        Task<bool> Adicionar(Categoria categoria);
        Task<bool> Alterar(Categoria categoria);
        Task<bool> Excluir(long id);
    }
}