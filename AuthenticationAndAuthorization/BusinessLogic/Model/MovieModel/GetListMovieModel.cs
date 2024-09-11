using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model.MovieModel
{
    public record GetListMovieModel(string name, int? minViews,List<string> genres);
}
