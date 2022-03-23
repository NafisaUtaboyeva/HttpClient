using httpclieent.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace httpclieent
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient httpClient;
        public MovieService()
        {
            httpClient = new HttpClient();
        }
        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            string json = JsonConvert.SerializeObject(movie);

            HttpContent content = new StringContent(json);

            HttpResponseMessage response = await httpClient.PostAsync(Constants.MOVIE_POST, content);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<bool> DeleteMovieAsync(long id)
        {
            string api = Constants.MOVIE_DELETE + $"?movieId={id}";

            HttpResponseMessage response = await httpClient.DeleteAsync(api);

            if(response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task GetImageAsync(long id, string filePath)
        {
            string api = Constants.MOVIE_GET_IMAGE + $"?movieId={id}";

            await File.WriteAllBytesAsync(filePath, await httpClient.GetByteArrayAsync(api));
        }

        public async Task<Movie> GetMovieAsync(long id)
        {
            string api = Constants.MOVIE_GET + $"?movieId={id}";

            HttpResponseMessage response = await httpClient.GetAsync(api);

            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Movie>( await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(Constants.MOVIE_GET_ALL);

            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Movie>>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<Movie> SetImageAsync(long id, string imageUrl)
        {
            string api = Constants.MOVIE_SET_IMAGE + $"?movieId={id}";
            var image = File.ReadAllBytes(imageUrl);

            var content = new MultipartContent();
            content.Add(new ByteArrayContent(image));

            HttpResponseMessage response = await httpClient.PostAsync(api, content);
            
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<Movie> UpdateMovieAsync(long id, Movie movie)
        {
            string api = Constants.MOVIE_UPDATE + $"?movieId={id}";
            string json = JsonConvert.SerializeObject(movie);

            HttpContent content = new StringContent(json);
            HttpResponseMessage response = await httpClient.PutAsync(api, content);

            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            return null;
        }
    }
}
