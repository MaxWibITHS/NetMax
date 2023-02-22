
using Microsoft.EntityFrameworkCore;

namespace VOD.Database.Contexts
{
    public class VODContext : DbContext
    {
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Film> Films => Set<Film>();
        public DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<SimilarFilm> SimilarFilms => Set<SimilarFilm>();

        public VODContext(DbContextOptions<VODContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
			/* Composit Keys */
			modelBuilder.Entity<SimilarFilm>().HasKey(ci => new { ci.FilmId, ci.SimilarFilmId });
			modelBuilder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });

			/* Configuring related tables for the Film table*/
			modelBuilder.Entity<Film>(entity =>
			{
				entity
					// For each film in the Film Entity,
					// reference relatred films in the SimilarFilms entity
					// with the ICollection<SimilarFilms>
					.HasMany(d => d.SimilarFilms)
					.WithOne(p => p.Film)
					.HasForeignKey(d => d.FilmId)
					// To prevent cycles or multiple cascade paths.
					// Neded when seeding similar films data.
					.OnDelete(DeleteBehavior.ClientSetNull);

				// Configure a many-to-many realtionship between genres
				// and films using the FilmGenre connection entity.
				entity.HasMany(d => d.Genres)
					  .WithMany(p => p.Films)
					  .UsingEntity<FilmGenre>()
					
					  // Specify the table name for the connection table
					  // to avoid duplicate tables (FilmGenre and FilmGenres)
					  // in the database.
					  .ToTable("FilmGenres");

			});
		}
    }
}



