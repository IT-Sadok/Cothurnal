using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public class MovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MovieService
    {
        public MovieDto ToDto(Movie movie)
        {
            return new MovieDto
            {
                Name = movie.Name,
                Description = movie.Description
            };
        }

        public Movie ToModel(MovieDto movieDto)
        {
            return new Movie(movieDto.Name,movieDto.Description);
        }
    }
}
