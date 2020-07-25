using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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

            services.AddSwaggerConfig();

            // services.AddSwaggerGen(s => 
            // {
            //     s.SwaggerDoc("v1", new OpenApiInfo { Title = "Siscs API", Version = "v1"} );
            // });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfig(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfig(provider);

            // app.UseSwagger();

            // app.UseSwaggerUI(s =>
            // {
            //     s.SwaggerEndpoint("/swagger/v1/swagger.json","Siscs API V1");
            // });

        }
    }
}
