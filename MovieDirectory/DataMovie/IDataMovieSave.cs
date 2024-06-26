using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public interface IDataMovieSave
    {
        public void SaveMovieToDb(int id, Movie movie);
        public void DeleteMovieFromDb(int id);
        public void UptadeMovieFromDb(int id, string newDescription);
        public IReadOnlyDictionary<int, Movie> ReadOnlyDictOfMoviesJson();
    }
}
