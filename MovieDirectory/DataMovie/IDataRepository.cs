using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public interface IDataRepository
    {
        public void SaveMovie(int id, Movie movie);
        public void DeleteMovie(int id);
        public void UpdateMovie(int id, string newDescription);
        public IReadOnlyDictionary<int, Movie> GetMovies();
    }
}
