using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Create : IActionMovie<string>
    {
        public void Action(Dictionary<int, Movie> movies, params string[] data)
        {
            var id = int.Parse(data[0]);
            var name = data[1];
            var description = data[2];

            movies.Add(id,new Movie(name, description));
        }
    }
}
