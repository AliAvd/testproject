using CodeFirst2.Models;
using System.Text.Json;

namespace CodeFirst2.Repository
{
    public class RequestRepo: IRequestRepo
    {
        private readonly HttpClient _httpClient;
        public RequestRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetMovie(string name)
        {
            var moviename = name;
            var url = $"https://moviesapi.ir/api/v1/movies?q={moviename}";
            var response = _httpClient.GetAsync(url);
            var responsebody = response.Result.Content.ReadAsStringAsync().Result;
            return responsebody;


        }
    }
}