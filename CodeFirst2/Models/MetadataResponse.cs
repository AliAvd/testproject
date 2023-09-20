namespace CodeFirst2.Models
{
    public class MetadataResponse
    {
        public string current_page { get; set; }
        public int per_page { get; set; }
        public int page_count { get; set; }
        public int total_count { get; set; }
    }
}
