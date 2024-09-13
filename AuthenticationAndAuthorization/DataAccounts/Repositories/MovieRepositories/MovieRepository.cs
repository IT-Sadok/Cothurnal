using DataAccounts.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccounts.Repositories.MovieRepositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int id, string newDescription)
        {
            await _context.Movies
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s =>
                s.SetProperty(m => m.Description, newDescription));
        }

        public async Task DeleteMovieAsync(int id)
        {
            await _context.Movies
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie != null)
            {
                movie.Views++;
                await _context.SaveChangesAsync();
            }

            return movie;
        }

        public async Task<List<Movie>> GetMoviesListAsync(GetListMovieModel filtrModel)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filtrModel.name))
            {
                query = query.Where(m => m.Name.Contains(filtrModel.name));
            }

            if (filtrModel.minViews != null)
            {
                query = query.Where(m => m.Views >= filtrModel.minViews);
            }

            if (filtrModel.genres != null && filtrModel.genres.Any())
            {
                query = query.Where(m => m.MovieGenres.Any(mg => filtrModel.genres.Contains(mg.Genre.Name)));
            }

            query = query.Skip((filtrModel.pageNumber - 1) * filtrModel.pageSize).Take(filtrModel.pageSize);

            return await query.Include(m => m.MovieGenres)
                              .ThenInclude(mg => mg.Genre)
                              .ToListAsync();
        }

        public async Task AddGenresToMovieAsync(int movieId, List<int> genreIds)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            foreach (var genreId in genreIds)
            {
                if (!movie.MovieGenres.Any(mg => mg.GenreId == genreId))
                {
                    movie.MovieGenres.Add(new MovieGenres { MovieId = movieId, GenreId = genreId });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
