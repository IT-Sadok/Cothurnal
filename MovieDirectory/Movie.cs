using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    class Movie
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Movie(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
