using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.DatabaseContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MedalhaEntity> Medalhas { get; set; }
    }
}
