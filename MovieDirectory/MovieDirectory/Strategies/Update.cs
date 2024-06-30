using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    class Update : StrategyBase, IActionMovie<UpdateMovieModel>
    {
        public void Action(IDataRepository format, UpdateMovieModel data)
        {
            format.UpdateMovie(data.id,data.description);
        }
    }
}
