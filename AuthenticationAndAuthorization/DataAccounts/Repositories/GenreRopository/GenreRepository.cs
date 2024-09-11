using DataAccounts.Entitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.GenreRopository;
using DataAccounts.Repositories.MovieEntitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories.GenreRepositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int id)
        {
            await _context.Genres
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task AddGenresAsync(int movieId, Genre genre)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            movie.MovieGenres.Add(new MovieGenres { Movie = movie, Genre = genre });

            await _context.SaveChangesAsync();
        }

        public async Task<List<GenreDto>> GetGenresAsync()
        {
            return await _context.Genres
                .Select(m => new GenreDto
                {
                    Id = m.Id,
                    Name = m.Name,
                })
                .ToListAsync();
        }
    }
}
