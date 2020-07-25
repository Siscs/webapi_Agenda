using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Siscs.Agenda.Api.Configuration;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<SiscsContext>(x => 
                x.UseSqlServer(Configuration.GetConnectionString("local"))
            );
        
            services.ResolveDependencies();

            services.AddIdentityConfiguration(Configuration);

            services.AddAutoMapper(typeof(Startup));
            
            services.AddApiConfiguration(Configuration);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfig(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
