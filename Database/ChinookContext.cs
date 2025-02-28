using Microsoft.EntityFrameworkCore;
using ChinookEntities;

namespace ChinookContext {

    public class ChinookDatabase : DbContext 
    {
        public DbSet<Album> Albums { get; set; }    
        public DbSet<Artist> Artists { get; set; }
        public DbSet<AlbumArtist> AlbumArtists { get; set; }
        public DbSet<Tracks> Tracks { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Data Source=chinook.db");
            
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // primary key for tracks within albums using the aldbum id and the trackid 
            modelBuilder.Entity<Tracks>()
            .HasKey(t => new { t.TrackId });

            modelBuilder.Entity<AlbumArtist>().HasNoKey();
        }
       

    }
}