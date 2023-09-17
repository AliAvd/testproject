using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst2.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        private Genre()
        {

        }
        public Genre(string name)
        {
            Name = name;
        }
        public static List<Genre> Genres = new List<Genre>
        {

        };
    }
}
