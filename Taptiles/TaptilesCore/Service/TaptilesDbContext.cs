using Microsoft.EntityFrameworkCore;
using Taptiles.Entity;

namespace Taptiles.Service
{
    public class TaptilesDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Rating> Star { get; set; }
        public DbSet<Coment> Coments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Taptiles;Trusted_Connection=True;");
        }
    }
}

