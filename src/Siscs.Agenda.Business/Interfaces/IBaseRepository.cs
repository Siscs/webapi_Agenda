using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> Obter();
        Task<TEntity> ObterPorId(long id);
        Task Adicionar(TEntity entity);
        Task Alterar(TEntity entity);
        Task Excluir(long id);

    }
}