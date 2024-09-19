using DataAccounts;
using DataAccounts.Entitys.MovieEntitys;
using DataAccounts.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataAccounts.Repositories;
using System.Data;

namespace DataLoader
{
    public class DataLoaderService
    {
        private readonly string _jsonData = Path.Combine(Directory.GetCurrentDirectory(),"data.json");
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepositories;

        public DataLoaderService(ApplicationDbContext context, IUserRepository userRepositories)
        {
            _context = context;
            _userRepositories = userRepositories;
        }

        public async Task LoadDataAsync()
        {
            var jsonData = File.ReadAllText(_jsonData);
            var rootJson = JsonConvert.DeserializeObject<RootJson>(jsonData);

            await LoadUsersAsync(rootJson.Users);
            await LoadGenresAsync(rootJson.Genres);
            await LoadMoviesAsync(rootJson.Movies);
        }

        public async Task LoadUsersAsync(List<User> users)
        {
            foreach (var userJson in users)
            {
                try
                {
                    string role = UserConstants.User;

                    var user = new User
                    {
                        UserName = userJson.UserName,
                        Email = userJson.Email,
                        ExternalId = userJson.Id.ToString(),
                    };

                    if (user.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
                    {
                        role = UserConstants.Admin;
                    }

                    await _userRepositories.Register(user, userJson.PasswordHash, role);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task LoadGenresAsync(List<Genre> genres)
        {
            foreach (var genreJson in genres)
            {
                try
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreJson.Name);
                    if (genre == null)
                    {
                        genre = new Genre(genreJson.Name) { ExternalId = genreJson.Id.ToString() };
                        _context.Genres.Add(genre);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public async Task LoadMoviesAsync(List<Movie> movies)
        {
            foreach (var movieJson in movies)
            {
                try
                {
                    var movie = await _context.Movies.FirstOrDefaultAsync(g => g.Name == movieJson.Name);

                    movie = new Movie(movieJson.Name, movieJson.Description)
                    {
                        Views = movieJson.Views,
                        ExternalId = movieJson.Id.ToString(),
                        MovieGenres = movieJson.MovieGenres.Select(genre => new MovieGenres { GenreId = genre.GenreId }).ToList()
                    };

                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
