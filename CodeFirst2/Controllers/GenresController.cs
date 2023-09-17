using CodeFirst2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public GenresController(ApplicationDbContext databaseContext)
        {
            dbContext = databaseContext;
        }

        [HttpGet()]
        public ActionResult GetGenres()
        {
            //if (Genre.Genres == null)
            //{
            //    return BadRequest();
            //}
            //return Ok(Genre.Genres);
            if (dbContext.Genres == null)
            {
                return BadRequest();
            }
            return Ok(dbContext.Genres);
        }
        [HttpGet("{genreId}/movies")]
        public ActionResult GetMoviesByGenre(int genreId)
        {
            //List<Movie> selectedMovies = new List<Movie>();
            //foreach (var movie in Movie.movieList)
            //{
            //    foreach (var genre in movie.Genres)
            //    {
            //        if (genre.Id == genreId)
            //        {
            //            selectedMovies.Add(movie);
            //        }
            //    }
            //}
            //if (selectedMovies.Count == 0)
            //{
            //    return BadRequest();
            //}
            //return Ok(selectedMovies);

            var selectedMovies = dbContext.MovieGenres.Where(i => i.GenreId == genreId).ToList();
            if (selectedMovies.Count == 0)
            {
                return BadRequest();
            }
            return Ok(selectedMovies);
        }
        [HttpPost()]
        public ActionResult PostGenre([FromBody] Genre newGenre)
        {
            if (newGenre == null)
            {
                return BadRequest();
            }
            Console.WriteLine(newGenre.Name);
            if (dbContext.Genres.Where(i => i.Name == newGenre.Name).ToList().Count() == 0)
            {
                dbContext.Add(newGenre);
                dbContext.SaveChanges();
                return Ok();
            }


            return BadRequest();
        }
    }
}
