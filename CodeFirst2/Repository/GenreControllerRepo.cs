using CodeFirst2.Models;

namespace CodeFirst2.Repository
{
    public class GenreControllerRepo : IGenreControllerRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public GenreControllerRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Genre AddGenre(Genre newgenre)
        {
            var genre = _dbContext.Genres.FirstOrDefault(x => x.Name == newgenre.Name);
            if (genre == null)
            {
                _dbContext.Genres.Add(newgenre);
                _dbContext.SaveChanges();
            }
            return newgenre;
        }

        public List<Genre> GetGenres()
        {
            return _dbContext.Genres.ToList();
        }

        public List<Movie> GetMoviesByGenre(int genreId)
        {
            var query = from movie in _dbContext.Movies
                        join movieGenre in _dbContext.MovieGenres
                        on movie.Id equals movieGenre.MovieId
                        where movieGenre.GenreId == genreId
                        select movie;
            return query.ToList();
        }

        /*public List<Movie> GetMoviesByGenre(int genreId)
        {
            var query = from movie in _dbContext.Movies
                        join genre in _dbContext.Genres
                        join
                        select new
                        {
                            MovieTitle = movie.Title,
                            GenreName = genre.Name
                        };
        }*/
    }
}
