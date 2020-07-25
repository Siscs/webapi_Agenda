using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Siscs.Agenda.Api.Data;
using Siscs.Agenda.Api.Extensions;
using Siscs.Agenda.Api.Services;

namespace Siscs.Agenda.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            // identity
            services.AddDbContext<ApiIdentityContext>(x => 
                x.UseSqlServer(configuration.GetConnectionString("local"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApiIdentityContext>()
                .AddErrorDescriber<IdentityPortugues>()
                .AddDefaultTokenProviders();


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
        
    }
}