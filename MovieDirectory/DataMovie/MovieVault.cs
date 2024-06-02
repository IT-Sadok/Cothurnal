using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDirectory;

namespace DataMovie
{
    public class MovieVault : IMovieManager
    {
        public Dictionary<int, Movie> movies { get; }
        public MovieVault()
        {
            movies = new Dictionary<int, Movie>();
        }
        public void CreateMovie(string name, int id, string desc)
        {
            movies.Add(id, new Movie(name, desc));
        }


        public void ListMovies()
        {
            if (movies.Count > 0)
            {
                foreach (var movie in movies)
                {
                    Console.WriteLine($"ID: {movie.Key}, Name: {movie.Value.Name}");
                }
            }
            else
                Console.WriteLine("the list is empty!");
        }

        public void ShowMovie(int id)
        {

            if (movies.TryGetValue(id, out Movie movie))
            {
                Console.WriteLine($"Name: {movie.Name}\nDescription: {movie.Description}");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public void UpdateMovie(int id, string newDesс)
        {
            movies[id].Description = newDesс;
        }

        public void DeleteMovie(int id)
        {
            movies.Remove(id);
        }
    }
}
