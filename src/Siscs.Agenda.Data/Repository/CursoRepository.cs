using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Data.Repository
{
    public class CursoRepository : BaseRepository<Curso>, ICursoRepository
    {
        public CursoRepository(SiscsContext context) : base(context)
        {
        }

        public override async Task<List<Curso>> Obter()
        {

            return  await _context.Cursos
                .Include(p => p.Categoria)
                .Include(x => x.Instituicao)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Curso> ObterPorId(long id)
        {
            return await _context.Cursos
                .Include(p => p.Categoria)
                .Include(x => x.Instituicao)
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}