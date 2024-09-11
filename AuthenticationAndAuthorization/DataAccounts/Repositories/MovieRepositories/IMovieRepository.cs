using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.MovieEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories.MovieRepositories
{
    public interface IMovieRepository
    {
        Task CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(int id, string newDescription);
        Task DeleteMovieAsync(int id);
        Task<List<Movie>> GetMoviesListAsync(string name, int? minViews, List<string> genres);
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task AddGenresToMovieAsync(int movieId, List<int> genreIds);
    }
}
