using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    internal class Create : IActionMovie<CreateMovieModel>
    {
        public void Action(IDataMovieSave format, CreateMovieModel data)
        {
            format.SaveMovieToDb( data.Id ,new Movie(data.Name, data.Description));
        }
    }
}
