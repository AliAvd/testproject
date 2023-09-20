using CodeFirst2.Models;

namespace CodeFirst2.Repository
{
    public interface IGenreControllerRepo
    {
        public List<Genre> GetGenres();
        public Genre AddGenre(Genre newgenre);
        public List<Movie> GetMoviesByGenre(int genreId);

    }
}
