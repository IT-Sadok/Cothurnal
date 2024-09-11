using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.MovieEntitys;
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

        public async Task<List<Movie>> GetMoviesListAsync(string name, int? minViews, List<string> genres)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            if (minViews != null)
            {
                query = query.Where(m => m.Views >= minViews);
            }

            if (genres != null && genres.Any())
            {
                query = query.Where(m => m.MovieGenres.Any(mg => genres.Contains(mg.Genre.Name)));
            }

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
