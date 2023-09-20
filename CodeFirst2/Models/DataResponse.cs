namespace CodeFirst2.Models
{
    public class DataResponse
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string poster { get; set; }
        public string year { get; set; }
        public string country { get; set; }
        public string imdb_rating { get; set; }
        public List<string> genres { get; set; }
        public List<string> images { get; set; }

    }
}
