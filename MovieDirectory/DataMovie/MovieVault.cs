using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public class MovieVault 
    {
        private Dictionary<int, Movie> _movies;
        public MovieVault()
        {
            _movies = new Dictionary<int, Movie>();
        }

        public Dictionary<int, Movie> GetMovies()
        {
            return _movies;
        }

        public IReadOnlyDictionary<int, Movie> GetList() { return _movies; }

        public bool KeyExists(int id)
        {
            return _movies.ContainsKey(id); 
        }
    }
}
