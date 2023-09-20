using CodeFirst2.Models;
using CodeFirst2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IGenreControllerRepo _genreControllerRepo;
        private static ILogger<GenresController> _logger;

        public GenresController(ApplicationDbContext databaseContext, IGenreControllerRepo genreControllerRepo, ILogger<GenresController> logger)
        {
            dbContext = databaseContext;
            _genreControllerRepo = genreControllerRepo;
            _logger = logger;
        }

        [HttpGet()]
        public ActionResult GetGenres()
        {
            _logger.LogInformation("Requested Getting Genres !");
            if (_genreControllerRepo.GetGenres() == null)
            {
                _logger.LogWarning("No Genres Found !");
                return BadRequest("No Genres!!!");
            }
            _logger.LogInformation("Genres have been returned !");
            return Ok(_genreControllerRepo.GetGenres());
        }
        [HttpGet("{genreId}/movies")]
        public ActionResult GetMoviesByGenre(int genreId)
        {
            _logger.LogInformation("Movies with genreId {id} have been requested!",genreId);
            return Ok(_genreControllerRepo.GetMoviesByGenre(genreId));
        }
        [HttpPost()]
        public ActionResult PostGenre([FromBody] Genre newGenre)
        {
            _logger.LogInformation("Requested to post genre with name {name}", newGenre.Name);
            return Ok(_genreControllerRepo.AddGenre(newGenre));
        }
    }
}
