using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Update : IActionMovie<UpdateMovieModel>
    {
        public void Action(IDataMovieSave format, UpdateMovieModel data)
        {
            format.UptadeMovieFromDb(data.Id,data.Description);
        }
    }
}
