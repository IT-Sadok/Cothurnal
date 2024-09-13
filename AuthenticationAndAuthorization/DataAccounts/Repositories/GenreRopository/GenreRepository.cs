﻿using DataAccounts.Entitys;
using DataAccounts.Entitys.GenreEntitys;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Repositories.GenreRopository;
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

        public async Task<List<Genre>> GetGenresAsync(GetGenresListModel filterModel)
        {
            var query = _context.Genres.AsQueryable();

            var totalCount = await query.CountAsync();

            return await query
                .Skip((filterModel.pageNumber - 1) * filterModel.pageSize)
                .Take(filterModel.pageSize)
                .ToListAsync();
        }
    }
}