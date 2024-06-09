using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public class MovieVault : Dictionary<int, Movie>
    {
        public Dictionary<int, Movie> _movies { get; set; }
        public MovieVault()
        {
            _movies = new Dictionary<int, Movie>();
        }

        public IReadOnlyDictionary<int, Movie> GetList() { return _movies; }

        public bool KeyExists(int id)
        {
            return _movies.ContainsKey(id); 
        }
    }
}
