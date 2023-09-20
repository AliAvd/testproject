using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst2.Models
{
    public class MovieGenres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        private MovieGenres()
        {

        }
        public MovieGenres(int movieId, int genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }
    }


}
