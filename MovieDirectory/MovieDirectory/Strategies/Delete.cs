using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    class Delete : StrategyBase, IActionMovie<DeleteMovieModel>
    {
        public void Action(IDataRepository format, DeleteMovieModel data)
        {
            var id = data.id;

            format.DeleteMovie(id);
        }
    }
}
