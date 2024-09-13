using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Entitys
{
    public record GetListMovieModel(string name, int? minViews,List<string> genres, int pageNumber, int pageSize);
}
