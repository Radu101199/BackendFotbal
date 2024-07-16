using AspNetCoreApp1.Models.Enitities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApp1.DataContext
{
    public class TeamAppDb: DbContext
    {
        public TeamAppDb(DbContextOptions<TeamAppDb> options): base(options)
        {

        }

        public DbSet<Tari> Tari { get; set; }
        public DbSet<Locatii> Locatii { get; set; }
        
        public DbSet<Campionate> Campionate { get; set; }

        public DbSet<Stadioane> Stadioane { get; set; }

        public DbSet<IstoricEchipe> IstoricEchipe { get; set; }

        public DbSet<Echipe> Echipe { get; set; }
        
        public DbSet<Jucatori> Jucatori { get; set; }
    }
}