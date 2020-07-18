using Siscs.Agenda.Business.Entities;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Data.Repository
{
    public class InstituicaoRepository : BaseRepository<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(SiscsContext context) : base(context) {}

    }
}