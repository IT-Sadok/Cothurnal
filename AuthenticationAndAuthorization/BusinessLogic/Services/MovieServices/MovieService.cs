using BusinessLogic.Interfaces;
using BusinessLogic.Model.GenreModel;
using BusinessLogic.Model.MovieModel;
using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.GenreRopository;
using DataAccounts.Repositories.MovieEntitys;
using DataAccounts.Repositories.MovieRepositories;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task CreateMovieAsync(CreateMovieModel createModel)
        {
            var movie = new Movie(createModel.id, createModel.name, createModel.description)
            {
                MovieGenres = createModel.genresId.Select(id =>
                new MovieGenres { GenreId = id }).ToList()
            };

            await _movieRepository.CreateMovieAsync(movie);
        }
            
        public async Task UpdateMovieAsync(UpdateMovieModel updateModel)
        {
            await _movieRepository.UpdateMovieAsync(updateModel.id,updateModel.newDescription);
        }

        public async Task DeleteMovieAsync(DeleteMovieModel deleteModel)
        {
            await _movieRepository.DeleteMovieAsync(deleteModel.id);
        }

        public async Task<List<MovieInfo>> GetMoviesListAsync(GetListMovieModel filtrModel)
        {
            var listOfMovie =  await _movieRepository.GetMoviesListAsync(filtrModel.name, filtrModel.minViews, filtrModel.genres);

            return listOfMovie
                .Select(movie => new MovieInfo
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Views = movie.Views,
                    Description = movie.Description,
                    Genres = movie.MovieGenres.Select(mg => mg.Genre.Name).ToList()
                }).ToList();
        }

        public async Task<MovieInfo> GetMovieAsync(GetMovieModel getModel)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(getModel.movieId);

            return new MovieInfo
            {
                Id = movie.Id,
                Name = movie.Name,
                Views = movie.Views,
                Description = movie.Description,
                Genres = movie.MovieGenres.Select(mg => mg.Genre.Name).ToList()
            };
        }

        public async Task AddGenresAsync(AddGenreModel addGenreModel)
        {
            await _movieRepository.AddGenresToMovieAsync(addGenreModel.movieId, addGenreModel.genreIds);
        }
    }
}
