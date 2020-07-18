using Microsoft.EntityFrameworkCore;
using Siscs.Agenda.Business.Entities;

namespace Siscs.Agenda.Data.Context
{
    public class SiscsContext : DbContext
    {
        public SiscsContext(DbContextOptions<SiscsContext> options)
            :base(options)
        {
        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder) {

            base.OnModelCreating(builder);
        }

    }
}