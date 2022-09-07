using Coodesh.Data.Mappings;
using Coodesh.Models;
using Microsoft.EntityFrameworkCore;

namespace Coodesh.Data
{
    
    public class CoodeshDbContext : DbContext
    {
        public CoodeshDbContext(DbContextOptions<CoodeshDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuariosMap());
        }
    }

}
