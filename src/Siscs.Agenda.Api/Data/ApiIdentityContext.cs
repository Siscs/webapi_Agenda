
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Siscs.Agenda.Api.Data
{
    public class ApiIdentityContext : IdentityDbContext
    {
        public ApiIdentityContext(DbContextOptions<ApiIdentityContext> options) : base(options)
        {
            
        }
    }
}