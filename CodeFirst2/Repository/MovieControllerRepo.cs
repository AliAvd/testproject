using CodeFirst2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CodeFirst2.Repository
{
    public class MovieControllerRepo : IMovieControllerRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRequestRepo _requestRepo;
        private readonly ILogger<MovieControllerRepo> _logger;
        public MovieControllerRepo(ApplicationDbContext dbContext, IRequestRepo requestRepo, ILogger<MovieControllerRepo> logger) { 
            _dbContext = dbContext;
            _requestRepo = requestRepo;
            _logger = logger;
        }

        public void AddMovie(Movie newmovie)
        {
            if (newmovie == null)
            {
                _logger.LogWarning("The Movie is null !");
            }
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Name == newmovie.Name);
            if (movie != null)
            {
                _logger.LogWarning("This movie is already in the Database !");
                return;
            }
            var genres = new List<Genre>();
            foreach (var genre in newmovie.Genres)
            {
                var _genre = _dbContext.Genres.FirstOrDefault(g => g.Name == genre.Name);
                if (_genre == null)
                {
                    _dbContext.Genres.Add(genre);
                    genres.Add(genre);
                    
                }
                else
                {
                    genres.Add(_genre);
                }
                _dbContext.SaveChanges();
            }
            
            newmovie.Genres = new List<Genre>();
            _dbContext.Movies.Add(newmovie);
            _dbContext.SaveChanges();
            _logger.LogInformation("Movie has been added to Database Successfuly !");
            foreach (Genre genre in genres)
            {
                var movieGenre = new MovieGenres(newmovie.Id, genre.Id);
                _dbContext.MovieGenres.Add(movieGenre);
            }
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(string name)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Name == name);
            if (movie == null)
            {
                _logger.LogWarning("No movie with this name is in Database !");
                return;
            }
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            _logger.LogInformation("The movie has been deleted sucessfully !");
        }

        public Movie GetMovieById(int id)
        {
            return (_dbContext.Movies.SingleOrDefault(x => x.Id == id));

        }

        public List<Movie> GetMovieByName(string? name, string? rate)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rate))
            {
                return _dbContext.Movies.ToList();
            }
            else if (name != null)
            {
                Movie movie = _dbContext.Movies.FirstOrDefault(x => x.Name == name);

                if (movie == null)
                {
                    string response = _requestRepo.GetMovie(name);
                    MovieResponse? movieResponse = JsonSerializer.Deserialize<MovieResponse>(response);
                    try
                    {
                        foreach (DataResponse data in movieResponse.data)
                        {
                            if (name == data.title)
                            {
                                Movie movieTobeAdded = new Movie(data.title, int.Parse(data.year), data.imdb_rating);
                                if (data.genres == null)
                                {
                                    continue;
                                }

                                foreach (string genre in data.genres)
                                {
                                    if (_dbContext.Genres.FirstOrDefault(x => x.Name == genre) == null)
                                    {
                                        _dbContext.Genres.Add(new Genre(genre));
                                    }
                                }
                                _dbContext.SaveChanges();
                                _dbContext.Movies.Add(movieTobeAdded);
                                _dbContext.SaveChanges();
                                int movieTobeAddedId = _dbContext.Movies.FirstOrDefault(x => x.Name == movieTobeAdded.Name).Id;
                                foreach (string genre in data.genres)
                                {
                                    int genreid = _dbContext.Genres.FirstOrDefault(x => x.Name == genre).Id;
                                    MovieGenres movieGenre = new MovieGenres(movieTobeAddedId, genreid);
                                    _dbContext.MovieGenres.Add(movieGenre);
                                }
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return new List<Movie> { movie };
            }
            else
            {
                var selectedList = _dbContext.Movies.Where(x => x.rate >= double.Parse(rate)).ToList();
                if (selectedList.Count == 0)
                {
                    return null;
                }
                return selectedList;
            }
            //return _dbContext.Movies.FirstOrDefault(x => x.Name == name);
        }
    }
}