using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDirectory;

namespace MovieDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var movies = new MovieManager();
            movies.Run();
        }
    }
}