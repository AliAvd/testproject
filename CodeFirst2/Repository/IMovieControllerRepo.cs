using CodeFirst2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst2.Repository
{
    public interface IMovieControllerRepo
    {
        
        public List<Movie> GetMovieByName(string? name, string? rate);
        public Movie GetMovieById(int id);
        public void AddMovie(Movie newmovie);
        public void DeleteMovie(string name);
    }
}
