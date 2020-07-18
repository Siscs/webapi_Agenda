using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly SiscsContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(SiscsContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> Obter()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Alterar(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public virtual async Task Excluir(long id)
        {
            _dbSet.Remove(new TEntity { Id = id });

            await SaveChanges();

        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        
    }
}