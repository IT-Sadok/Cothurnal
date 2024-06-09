using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Delete : IActionMovie
    {
        public void Action(Dictionary<int, Movie> movies, params object[] data)
        {
            var id = (int)data[0];

            movies.Remove(id);
        }
    }
}
