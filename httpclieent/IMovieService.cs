using httpclieent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace httpclieent
{
    public interface IMovieService
    {
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(long id, Movie movie);
        Task<bool> DeleteMovieAsync(long id);
        Task<Movie> GetMovieAsync(long id);
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task GetImageAsync(long id, string filePath);
        Task<Movie> SetImageAsync(long id, string imageUrl);
    }
}
