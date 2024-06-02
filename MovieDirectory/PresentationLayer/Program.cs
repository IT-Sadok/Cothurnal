using System;
using MovieDirectory;
using DataMovie;


namespace MovieDirectory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var movies = new MovieVault();
            ChooseAnAction(movies);
        }
        public static void ChooseAnAction(MovieVault movies)
        {
            while (true)
            {
                Console.WriteLine("Choose an action:\n-create\n-read\n-update\n-delete\n-exit");
                var action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "create":
                        Create(movies);
                        break;
                    case "read":
                        Read(movies);
                        break;
                    case "update":
                        Update(movies);
                        break;
                    case "delete":
                        Delete(movies);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Incorrect input. Try again.");
                        break;
                }
            }
        }
        public static void Create(MovieVault movie)
        {
            Console.WriteLine("Enter movie name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the id of the film:");
            if (!int.TryParse(Console.ReadLine(), out int id) || movie.movies.ContainsKey(id))
            {
                Console.WriteLine("This ID already exists!");
                return;
            }

            Console.WriteLine("Enter a description of the movie:");
            string description = Console.ReadLine();

            movie.CreateMovie(name,id, description);
            Console.WriteLine("Film added.");
        }
        public static void Read(MovieVault movie)
        {
            Console.WriteLine("Pick out the action:\nview list of films (0)\nWatch the film by ID (1)");
            string action = Console.ReadLine().ToLower();
            if (int.TryParse(action, out int num))
            {
                ReadMode(num,movie);
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
        public static void ReadMode(int num, MovieVault movie)
        {
            switch (num)
            {
                case 0:
                    movie.ListMovies();
                    break;
                case 1:
                    Console.WriteLine("Enter ID:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        movie.ShowMovie(id);
                    else
                        Console.WriteLine("Incorrect input ID.");
                    break;
                default:
                    Console.WriteLine("Incorrect input.");
                    break;
            }
        }
        public static void Update(MovieVault movie)
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && movie.movies.ContainsKey(id))
            {
                Console.WriteLine("Enter new film description:");
                movie.UpdateMovie(id, Console.ReadLine());
                Console.WriteLine("Film description updated.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
        public static void Delete(MovieVault movie)
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && movie.movies.ContainsKey(id))
            {
                movie.DeleteMovie(id);
                Console.WriteLine("Film deleted.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
    }
}