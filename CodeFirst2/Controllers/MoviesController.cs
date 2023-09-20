using NLog;
using CodeFirst2.Models;
using CodeFirst2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private static ILogger<MoviesController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IMovieControllerRepo _movieControllerRepo;
        private readonly IRequestRepo _requestRepo;

        public MoviesController(ApplicationDbContext databaseContext, IMovieControllerRepo movieControllerRepo, ILogger<MoviesController> logger, IRequestRepo requestRepo)
        {
            dbContext = databaseContext;
            _movieControllerRepo = movieControllerRepo;
            _logger = logger;
            _requestRepo = requestRepo;
        }

        [HttpGet()]
        public ActionResult GetMovieByName([FromQuery] string? name, [FromQuery] string? rate)
        {
            _logger.LogInformation("Requested Get Movie API ! " + "Movie Name : " + name + " Rate : " + rate);
            var movies = _movieControllerRepo.GetMovieByName(name, rate);
            //if (movies == null)
            //{
            //    _logger.LogWarning("No movie Found !");
            //    return BadRequest("Not Found!!!");
            //}
            //_logger.LogInformation("Requested movies have been returned !");
            //return Ok(movies);
            
            return Ok(_movieControllerRepo.GetMovieByName(name,rate));
        }


        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            _logger.LogInformation("Requested movie by Id : {id}", id);
            var movie = _movieControllerRepo.GetMovieById(id);
            if (movie == null)
            {
                _logger.LogWarning("No movie Found !");
                return BadRequest("Not Found!!!");
            }
            _logger.LogInformation("Requested movie have been returned !");
            return Ok(movie);
        }
        [HttpPost()]
        public ActionResult AddMovie([FromBody] Movie newMovie)
        {
            _logger.LogInformation("Request Posting movie !");
            _movieControllerRepo.AddMovie(newMovie);
            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);

        }

        [HttpDelete("{name}")]
        public ActionResult DeleteMovie(string name)
        {
            _logger.LogInformation("Request deleting movie Name : {name}", name);
            _movieControllerRepo.DeleteMovie(name);
            return Ok();
        }

    }
}
