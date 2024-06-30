using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Create : StrategyBase, IActionMovie<CreateMovieModel>
    {
        public void Action(IDataRepository format, CreateMovieModel data)
        {
            format.SaveMovie( data.id ,new Movie(data.name, data.description));
        }
    }
}
