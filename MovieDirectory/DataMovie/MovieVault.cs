using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public class MovieVault 
    {
        public Dictionary<int, Movie> _movies { get; }
        public MovieVault()
        {
            _movies = new Dictionary<int, Movie>();
        }
        public void CreateMovie(string name, int id, string description)
        {
            _movies.Add(id, new Movie(name, description));
        }

        public void UpdateMovie(int id, string newDescription)
        {
            _movies[id].Description = newDescription;
        }

        public void DeleteMovie(int id)
        {
            _movies.Remove(id);
        }

        public bool KeyExists(int id)
        {
            return _movies.ContainsKey(0); 
        }
    }
}
