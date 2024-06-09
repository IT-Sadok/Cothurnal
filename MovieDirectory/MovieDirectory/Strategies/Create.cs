using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Create : IActionMovie
    {
        public void Action(Dictionary<int, Movie> movies, params object[] data)
        {
            var id = (int)data[0];
            var name = (string)data[1];
            var description = (string)data[2];

            movies.Add(id,new Movie(name, description));
        }
    }
}
