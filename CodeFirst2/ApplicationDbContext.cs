using CodeFirst2.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany()
                .UsingEntity<MovieGenres>(
                    mg => mg.HasOne(mg => mg.Genre).WithMany(),
                    mg => mg.HasOne(mg => mg.Movie).WithMany()
                );
            modelBuilder.Entity<MovieGenres>()
        .HasKey(mg => mg.Id);
            //modelBuilder.Entity<Genre>().HasIndex(u => u.Name).IsUnique();

        }


    }
}