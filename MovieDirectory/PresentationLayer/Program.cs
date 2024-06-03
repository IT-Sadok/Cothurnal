﻿using System;
using DataMovie;
using System.Runtime.CompilerServices;

namespace PresentationLayer
{
    internal class Program
    {
        private static MovieVault _movieVault = new MovieVault();

        static void Main(string[] args)
        {
            ChooseAnAction();
        }

        public static void ChooseAnAction()
        {
            while (true)
            {
                Console.WriteLine("Choose an action:\n-create\n-read\n-update\n-delete\n-exit");
                var action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "create":
                        Create();
                        break;
                    case "read":
                        Read();
                        break;
                    case "update":
                        Update();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Incorrect input. Try again.");
                        break;
                }
            }
        }

        public static void Create()
        {
            Console.WriteLine("Enter movie name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the id of the film:");
            if (!int.TryParse(Console.ReadLine(), out int id) || _movieVault._movies.ContainsKey(id))
            {
                Console.WriteLine("This ID already exists!");
                return;
            }

            Console.WriteLine("Enter a description of the movie:");
            string description = Console.ReadLine();

            _movieVault.CreateMovie(name, id, description);
            Console.WriteLine("Film added.");
        }

        public static void Read()
        {
            Console.WriteLine("Pick out the action:\nview list of films (0)\nWatch the film by ID (1)");
            string action = Console.ReadLine().ToLower();
            if (Enum.TryParse(action, out ReadMode mode))
            {
                ReadAction(mode);
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public static void ReadAction(ReadMode mode)
        {
            switch (mode)
            {
                case ReadMode.List:
                    OutputList();
                    break;
                case ReadMode.ById:
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

        public static void ShowMovie(int id)
        {
            if (_movieVault._movies.TryGetValue(id, out Movie movie))
            {
                Console.WriteLine($"Name: {movie.Name}\nDescription: {movie.Description}");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public static void OutputList()
        {
            if (_movieVault._movies.Count > 0)
            {
                foreach (var movie in _movieVault._movies)
                {
                    Console.WriteLine($"ID: {movie.Key}, Name: {movie.Value.Name}");
                }
            }
            else
                Console.WriteLine("the list is empty!");
        }

        public static void Update()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.KeyExists(id))
            {
                Console.WriteLine("Enter new film description:");
                _movieVault.UpdateMovie(id, Console.ReadLine());
                Console.WriteLine("Film description updated.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public static void Delete()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.KeyExists(id))
            {
                _movieVault.DeleteMovie(id);
                Console.WriteLine("Film deleted.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
    }
}