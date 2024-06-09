using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMovie;

namespace MovieDirectory
{ 
    public interface IActionMovie
    {
        public void Action(Dictionary<int, Movie> movies, params object[] data);
    }
}
