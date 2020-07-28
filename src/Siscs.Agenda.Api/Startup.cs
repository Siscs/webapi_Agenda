using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Siscs.Agenda.Api.Configuration;
using Siscs.Agenda.Api.Extensions;
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

            services.AddLoggerConfig(Configuration);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {

            app.UseApiConfig(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/api/hc", new HealthCheckOptions()
                // {
                //     Predicate = _ => true,
                //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                // });
                // endpoints.MapHealthChecksUI(options =>
                // {
                //     options.UIPath = "/api/hc-ui";
                //     options.ResourcesPath = "/api/hc-ui-resources";

                //     options.UseRelativeApiPath = false;
                //     options.UseRelativeResourcesPath = false;
                //     options.UseRelativeWebhookPath = false;
                // });
            });

            app.UseSwaggerConfig(provider);

            // app.UseSwagger();

            // app.UseSwaggerUI(s =>
            // {
            //     s.SwaggerEndpoint("/swagger/v1/swagger.json","Siscs API V1");
            // });

            app.UseLoggerConfig();

        }
    }
}
