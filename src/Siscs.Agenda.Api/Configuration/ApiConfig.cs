using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Siscs.Agenda.Api.Services;
using Siscs.Agenda.Data.Context;

namespace Siscs.Agenda.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("Desenvolvimento", p => p
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());

                options.AddPolicy("Producao", p => p
                .WithOrigins("https://siscs.com.br")
                .WithMethods("GET", "PUT", "POST")
                //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                .AllowAnyHeader()
                .AllowCredentials());

                options.AddDefaultPolicy(p => p
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());

                    
            });

            services.ResolveDependencies();

            // desativar modelstate validators
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // TOKEN JWT
            var tokenConfigSection = configuration.GetSection("TokenConfig");
            services.Configure<TokenConfig>(tokenConfigSection);

            var tokenConfig = tokenConfigSection.Get<TokenConfig>();
            var key = Encoding.ASCII.GetBytes(tokenConfig.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidIssuer = tokenConfig.Emissor,
                     ValidAudience = tokenConfig.ValidoEm
                };
            });

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseCors("Desenvolvimento");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Producao");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            return app;
            
        }


    }
}