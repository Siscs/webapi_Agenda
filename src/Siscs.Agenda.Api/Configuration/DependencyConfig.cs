using Microsoft.Extensions.DependencyInjection;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Data.Context;
using Siscs.Agenda.Data.Repository;

namespace Siscs.Agenda.Api.Configuration
{
    public static class DependencyConfig
    {

        public static void ResolveDependencies(this IServiceCollection services ) {

            services.AddScoped<SiscsContext, SiscsContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();

        }
        
    }
}