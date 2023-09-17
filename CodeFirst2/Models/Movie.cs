using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst2.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }
        public double rate { get; set; }
        public List<Genre> Genres { get; set; }
        private Movie()
        {

        }

        public Movie(string name, int year)
        {
            Name = name;
            Year = year;

            Random random = new Random();
            double minValue = 0.0;
            double maxValue = 5.0;

            rate = random.NextDouble() * (maxValue - minValue) + minValue;

        }

        public static List<Movie> movieList = new List<Movie>
        {
            new Movie("Hello1",1987),
            new Movie("Hello2",2002),
            new Movie("Hello3",2013)
        };
    }
}