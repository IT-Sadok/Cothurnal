using BusinessLogic.Interfaces;
using DataAccounts.Entitys.MovieEntitys;
using BusinessLogic.Model.GenreModel;
using DataAccounts.Repositories.GenreRopository;

namespace BusinessLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task CreateGenre(CreateGenreModel createModel)
        {
            await _genreRepository.AddGenreAsync(new Genre(createModel.id,createModel.name));
        }

        public async Task DeleteGenre(DeleteGenreModel deleteModel)
        {
            await _genreRepository.DeleteGenreAsync(deleteModel.id);
        }

        public async Task<List<GenreDto>> GetGenre()
        {
            return await _genreRepository.GetGenresAsync();
        }
    }
}
