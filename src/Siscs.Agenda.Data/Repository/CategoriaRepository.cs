using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Data.Repository
{
    
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(SiscsContext context) : base(context)
        {

        }
       
    }
}