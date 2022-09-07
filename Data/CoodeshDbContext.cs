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

        public DbSet<User> User { get; set; }
        public DbSet<Word> Word { get; set; }
        public DbSet<AccessHistory> AccessHistory { get; set; }
        public DbSet<FavoriteWord> FavoriteWord { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new WordsMap());
            modelBuilder.ApplyConfiguration(new AccessHistoriesMap());
            modelBuilder.ApplyConfiguration(new FavoriteWordsMap());
        }
    }

}
