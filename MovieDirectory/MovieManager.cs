using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class MovieManager
    {
        private Dictionary<int, Movie> movies = new Dictionary<int, Movie>();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Choose an action:\n-create\n-read\n-update\n-delete\n-exit");
                string action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "create":
                        CreateMovie();
                        break;
                    case "read":
                        ReadMovie();
                        break;
                    case "update":
                        UpdateMovie();
                        break;
                    case "delete":
                        DeleteMovie();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Incorrect input. Try again.");
                        break;
                }
            }
        }

        private void CreateMovie()
        {
            Console.WriteLine("Enter movie name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the id of the film:");
            if (!int.TryParse(Console.ReadLine(), out int id) || movies.ContainsKey(id))
            {
                Console.WriteLine("This ID already exists!");
                return;
            }

            Console.WriteLine("Enter a description of the movie:");
            string description = Console.ReadLine();

            movies.Add(id, new Movie(name, description));
            Console.WriteLine("Film added.");
        }

        private void ReadMovie()
        {
            Console.WriteLine("Pick out the action:\nview list of films (0)\nWatch the film by ID (1)");
            string action = Console.ReadLine().ToLower();

            switch (action)
            {
                case "0":
                    ListMovies();
                    break;
                case "1":
                    Console.WriteLine("Enter ID:");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        ShowMovie(id);
                    else
                        Console.WriteLine("Incorrect input ID.");
                    break;
                default:
                    Console.WriteLine("Incorrect input.");
                    break;
            }
        }

        private void ListMovies()
        {
            if (movies.Count > 0)
            {
                foreach (var movie in movies)
                {
                    Console.WriteLine($"ID: {movie.Key}, Name: {movie.Value.Name}");
                }
            }
            else
                Console.WriteLine("the list is empty!");
        }

        private void ShowMovie(int id)
        {
            if (movies.TryGetValue(id, out Movie movie))
            {
                Console.WriteLine($"Name: {movie.Name}\nDescription: {movie.Description}");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        private void UpdateMovie()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && movies.ContainsKey(id))
            {
                Console.WriteLine("Enter new film description:");
                movies[id].Description = Console.ReadLine();
                Console.WriteLine("Film description updated.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        private void DeleteMovie()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                movies.Remove(id);
                Console.WriteLine("Film deleted.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
    }
}

