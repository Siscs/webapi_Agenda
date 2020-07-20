using System.Threading.Tasks;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface IInstituicaoService
    {
        Task<bool> Adicionar(Instituicao categoria);
        Task<bool> Alterar(Instituicao categoria);
        Task<bool> Excluir(long id);
    }
}