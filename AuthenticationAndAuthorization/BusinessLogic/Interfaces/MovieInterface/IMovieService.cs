using BusinessLogic.Model.MovieModel;
using DataAccounts.Entitys;
using DataAccounts.Repositories.MovieEntitys;

namespace BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        Task CreateMovieAsync(CreateMovieModel createModel);
        Task UpdateMovieAsync(UpdateMovieModel updateModel);
        Task DeleteMovieAsync(DeleteMovieModel deleteModel);
        Task<MovieInfo> GetMovieAsync(GetMovieModel getModel);
        Task<List<MovieInfo>> GetMoviesListAsync(GetListMovieModel filtrModel);
        Task AddGenresAsync(AddGenreModel addGenreModel);
    }
}
