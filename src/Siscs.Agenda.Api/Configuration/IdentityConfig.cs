using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Siscs.Agenda.Api.Data;
using Siscs.Agenda.Api.Extensions;

namespace Siscs.Agenda.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApiIdentityContext>(x => 
                x.UseSqlServer(configuration.GetConnectionString("local"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApiIdentityContext>()
                .AddErrorDescriber<IdentityPortugues>()
                .AddDefaultTokenProviders();

            return services;
        }
        
    }
}