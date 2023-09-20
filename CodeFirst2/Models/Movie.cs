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

        public Movie(string name, int year, string ratee)
        {
            Name = name;
            Year = year;
            rate = double.Parse(ratee);

        }

        
    }
}