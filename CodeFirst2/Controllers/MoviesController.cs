using CodeFirst2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MoviesController(ApplicationDbContext databaseContext)
        {
            dbContext = databaseContext;
        }

        [HttpGet()]
        public ActionResult GetMovieByName([FromQuery] string? name, [FromQuery] string? rate)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rate))
            {
                return Ok(dbContext.Movies);
            }
            else if (string.IsNullOrEmpty(rate))
            {
                //var movie = Movie.movieList.Find(i => i.Name == name);
                var movie = dbContext.Movies.FirstOrDefault(x => x.Name == name);

                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }
            else
            {
                //List<Movie> selectedList = Movie.movieList.FindAll(movie => movie.rate >= double.Parse(rate));
                var selectedList = dbContext.Movies.Where(x => x.rate >= double.Parse(rate)).ToList();
                return Ok(selectedList);
            }

        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            //var movie = Movie.movieList.Find(i => i.Id == id);
            //if (movie == null)
            //{
            //    return BadRequest();
            //}
            //return Ok(movie);
            var movie = dbContext.Movies
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
        [HttpPost()]
        public ActionResult AddMovie([FromBody] Movie newMovie)
        {
            if (newMovie == null)
            {
                return BadRequest();
            }

            dbContext.Movies.Add(newMovie);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
        }

        [HttpDelete("{name}")]
        public ActionResult DeleteMovie(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var movie = dbContext.Movies.FirstOrDefault(i => i.Name == name);
            if (movie == null)
            {
                return NotFound();
            }
            dbContext.Movies.Remove(movie);
            var toBeDeleted = dbContext.MovieGenres.Where(i => i.Movie == movie).ToList();
            for (int i = 0; i < toBeDeleted.Count; i++)
            {
                dbContext.MovieGenres.Remove(toBeDeleted[i]);
            }
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
