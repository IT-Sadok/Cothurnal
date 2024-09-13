﻿using DataAccounts.Entitys.GenreEntitys;
using DataAccounts.Entitys.MovieEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories.GenreRopository
{
    public interface IGenreRepository
    {
        Task AddGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
        Task AddGenresAsync(int movieId, Genre genre);
        Task<List<Genre>> GetGenresAsync(GetGenresListModel filterModel);
    }
}