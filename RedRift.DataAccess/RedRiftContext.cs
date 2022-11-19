using Microsoft.EntityFrameworkCore;

using RedRift.Common.Model;

namespace RedRift.DataAccess
{
    public class RedRiftContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        // Use NoSql db, eg MongoDb
        public DbSet<GameResult> GameResults { get; set; }

        public RedRiftContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=RedRift.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().Ignore(nameof(Player.Health));
        }
    }
}
