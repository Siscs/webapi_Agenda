using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Business.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity: Entity
    {
        
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> Obter();
        Task<TEntity> ObterPorId(long id);
        Task Adicionar(TEntity entity);
        Task Alterar(TEntity entity);
        Task Excluir(long id);

    }
}