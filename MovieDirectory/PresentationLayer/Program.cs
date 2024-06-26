using System;
using DataMovie;
using MovieDirectory;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace PresentationLayer
{
    internal class Program
    {
        private IDataMovieSave _movieVault;
        private FormatType _format;
        private MovieManager<CreateMovieModel> _managerCreate;
        private MovieManager<UpdateMovieModel> _managerUpdate;
        private MovieManager<DeleteMovieModel> _managerDelete;

        public Program()
        {
            _managerCreate = new MovieManager<CreateMovieModel>();
            _managerUpdate = new MovieManager<UpdateMovieModel>();
            _managerDelete = new MovieManager<DeleteMovieModel>();
        }

        static void Main()
        {
            Program program = new Program();
            program.ChooseAnFormat();
            program.ChooseAnAction();
        }
        public void ChooseAnFormat()
        {
            Console.WriteLine("Choose an save format:\n-Json\n-CSV");
            var format = Console.ReadLine().ToLower();
            switch (format)
            {
                case "json":
                    _movieVault = new MovieVaultJson();
                    _format = FormatType.Json;
                    break;
                case "csv":
                    _movieVault = new MovieVaultCSV(); 
                    _format = FormatType.CSV;
                    break;
            }

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
            if (!int.TryParse(Console.ReadLine(), out int id) || _movieVault.ReadOnlyDictOfMoviesJson().ContainsKey(id))
            {
                Console.WriteLine("This ID already exists!");
                return;
            }

            Console.WriteLine("Enter a description of the movie:");
            string description = Console.ReadLine();

            _managerCreate.SetAction(MovieActionFactory.CreateAction<CreateMovieModel>(ActionType.Create));
            _managerCreate.SetFormat(DataMovieSaveFactory.GetInstance(_format));
            _managerCreate.ExecuteAction(new CreateMovieModel(id,name, description));
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
            if (_movieVault.ReadOnlyDictOfMoviesJson().TryGetValue(id, out Movie movie))
            {
                Console.WriteLine(movie);
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public void OutputList()
        {
            if (_movieVault.ReadOnlyDictOfMoviesJson().Count > 0)
            {
                foreach (var movie in _movieVault.ReadOnlyDictOfMoviesJson())
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
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.ReadOnlyDictOfMoviesJson().ContainsKey(id))
            {
                Console.WriteLine("Enter new film description:");
                _managerUpdate.SetAction(MovieActionFactory.CreateAction<UpdateMovieModel>(ActionType.Update));
                _managerUpdate.SetFormat(DataMovieSaveFactory.GetInstance(_format));
                _managerUpdate.ExecuteAction(new UpdateMovieModel(id,Console.ReadLine()));
                Console.WriteLine("Film description updated.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }

        public void Delete()
        {
            Console.WriteLine("Enter film ID:");
            if (int.TryParse(Console.ReadLine(), out int id) && _movieVault.ReadOnlyDictOfMoviesJson().ContainsKey(id))
            {
                _managerDelete.SetAction(MovieActionFactory.CreateAction<DeleteMovieModel>(ActionType.Delete));
                _managerDelete.SetFormat(DataMovieSaveFactory.GetInstance(_format));
                _managerDelete.ExecuteAction(new DeleteMovieModel(id));
                Console.WriteLine("Film deleted.");
            }
            else
                Console.WriteLine("Movie with such ID not found.");
        }
    }
}   