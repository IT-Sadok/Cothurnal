using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public interface IMovieManager
    {
        void CreateMovie(string name,int id,string desc);
        void ListMovies();
        void ShowMovie(int id);
        void UpdateMovie(int id, string newDesс);
        void DeleteMovie(int id);
    }
}
