﻿using System;
using DataMovie;
using MovieDirectory;
using System.Runtime.CompilerServices;

namespace PresentationLayer
{
    internal class Program
    {
        private MovieVault _movieVault;
        private MovieManager<string> _managerString;

        public Program()
        {
            _movieVault = new MovieVault();
            _managerString = new MovieManager<string>();
        }

        static void Main()
        {
            Program program = new Program();
            program.ChooseAnAction();
        }

        public void ChooseAnAction()
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

        public void Create()
        {
            Console.WriteLine("Enter movie name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the id of the film:");
            if (!int.TryParse(Console.ReadLine(), out int id) || _movieVault.ContainsKey(id))
            {
                Console.WriteLine("This ID already exists!");
                return;
            }

            Console.WriteLine("Enter a description of the movie:");
            string description = Console.ReadLine();

            _managerString.SetAction(MovieActionFactory.CreateAction<string>(ActionType.Create));
            _managerString.ExecuteAction(_movieVault, id.ToString(), name, description);
            Console.WriteLine("Film added.");
        }

        public void Read()
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

        public void ReadAction(ReadMode mode)
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

        public void ShowMovie(int id)
        {
            if (_movieVault.TryGetValue(id, out Movie movie))
            {
                Console.WriteLine(movie);
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public void OutputList()
        {
            if (_movieVault.Count > 0)
            {
                foreach (var movie in _movieVault)
                {
                    Console.WriteLine($"ID: {movie.Key}, Name: {movie.Value.Name}");
                }
            }
            else
                Console.WriteLine("the list is empty!");
        }

        public void Update()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.ContainsKey(id))
            {
                Console.WriteLine("Enter new film description:");
                _managerString.SetAction(MovieActionFactory.CreateAction<string>(ActionType.Update));
                _managerString.ExecuteAction(_movieVault,id.ToString(), Console.ReadLine());
                Console.WriteLine("Film description updated.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public void Delete()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.ContainsKey(id))
            {
                _managerString.SetAction(MovieActionFactory.CreateAction<string>(ActionType.Delete));
                _managerString.ExecuteAction(_movieVault, id.ToString());
                Console.WriteLine("Film deleted.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
    }
}