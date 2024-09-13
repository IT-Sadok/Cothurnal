﻿using BusinessLogic.Interfaces;
using DataAccounts.Entitys.MovieEntitys;
using BusinessLogic.Model.GenreModel;
using DataAccounts.Repositories.GenreRopository;
using AutoMapper;
using DataAccounts.Entitys.GenreEntitys;
using System.IO;

namespace BusinessLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task CreateGenre(CreateGenreModel createModel)
        {
            await _genreRepository.AddGenreAsync(new Genre(createModel.id, createModel.name));
        }

        public async Task DeleteGenre(DeleteGenreModel deleteModel)
        {
            await _genreRepository.DeleteGenreAsync(deleteModel.id);
        }

        public async Task<List<GenreDto>> GetGenre(GetGenresListModel filterModel)
        {
            var genres = await _genreRepository.GetGenresAsync(filterModel);
            return _mapper.Map<List<GenreDto>>(genres);
        }
    }
}