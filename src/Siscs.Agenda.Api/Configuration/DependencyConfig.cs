using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Siscs.Agenda.Api.Extensions;
using Siscs.Agenda.Business.Interfaces;
using Siscs.Agenda.Business.Notificacoes;
using Siscs.Agenda.Business.Services;
using Siscs.Agenda.Data.Context;
using Siscs.Agenda.Data.Repository;

namespace Siscs.Agenda.Api.Configuration
{
    public static class DependencyConfig
    {

        public static void ResolveDependencies(this IServiceCollection services ) {

            // context
            services.AddScoped<SiscsContext, SiscsContext>();

            // repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();

            // services
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            // services.AddScoped<IInstituicaoService, InstituicaoService>();
            // services.AddScoped<ICursoService, CursoService>();

            // app
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuario, AspNetUsuario>();




        }
        
    }
}