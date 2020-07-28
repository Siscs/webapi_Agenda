using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Siscs.Agenda.Api.Extensions;

namespace Siscs.Agenda.Api.Configuration
{
    public static class LoggerConfig
    {

        public static IServiceCollection AddLoggerConfig(this IServiceCollection services, IConfiguration configuration ) 
        {

            // elmah config
            // services.AddElmahIo(o =>
            // {
            //     o.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
            //     o.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
            // });

            var sqlConnection = configuration.GetConnectionString("local");
            
            
            //services.AddHealthChecks();

           
            services.AddHealthChecks()
            .AddCheck("Categorias", new SqlServerHealthCheck(sqlConnection))
            .AddSqlServer(sqlConnection, name: "BancoSql");

            // provider elmah com ilogger
            // services.AddHealthChecks()
            //     .AddElmahIoPublisher(options =>
            //     {
            //         options.ApiKey = "388dd3a277cb44c4aa128b5c899a3106";
            //         options.LogId = new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096");
            //         options.HeartbeatId = "API Fornecedores";

            //     })
            //     .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //     .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");
            //     acima add integração com haltchecks

             //services.AddHealthChecksUI();
                // .AddInMemoryStorage();
            

            return services;

        }


        public static IApplicationBuilder UseLoggerConfig(this IApplicationBuilder app)
        {

            //app.UseElmahIo();

            //app.UseHealthChecks("/api/hc");

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // app.UseHealthChecksUI(options => 
            // {
            //      options.UIPath = "/api/hc-ui";
            //      options.ApiPath = "/stt";
            // });

            return app;
            
        }



        
        
    }
}