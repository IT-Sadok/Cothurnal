using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Update : IActionMovie<string>
    {
        public void Action(Dictionary<int, Movie> movies, params string[] data)
        {
            var id = int.Parse(data[0]);
            var newDescription = data[1];
            movies[id].Description = newDescription;
        }
    }
}
