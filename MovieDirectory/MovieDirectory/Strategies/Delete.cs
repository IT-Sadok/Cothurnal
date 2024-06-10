using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{

    internal class Delete : IActionMovie<string>
    {
        public void Action(Dictionary<int, Movie> movies, params string[] data)
        {
            var id = data[0];

            movies.Remove(int.Parse(id));
        }
    }
}
