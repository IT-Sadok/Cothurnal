using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataMovie
{
    public class MovieVaultCSV : IDataRepository
    {
        private string _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "movies.csv");

        public MovieVaultCSV()
        {
            MoviesDictionary = new Dictionary<int, Movie>(GetMovies());
        }
        private Dictionary<int, Movie> MoviesDictionary { get; set; }

        public void DeleteMovie(int id)
        {
            MoviesDictionary.Remove(id);

            SaveDatabaseToFile();
        }

        public void SaveMovie(int id, Movie movie)
        {
            MoviesDictionary[id] = movie;

            SaveDatabaseToFile();
        }

        public void UpdateMovie(int id, string newDescription)
        {
            MoviesDictionary[id].Description = newDescription;

            SaveDatabaseToFile();
        }
        public IReadOnlyDictionary<int, Movie> GetMovies()
        {
            if (!File.Exists(_dbFilePath))
                return new Dictionary<int, Movie>();

            Dictionary<int, Movie> dictionary = new Dictionary<int, Movie>();
            StreamReader reader;

            using (reader = new StreamReader(_dbFilePath))
            {
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    int id = int.Parse(values[0]);
                    string name = values[1];
                    string description = values[2];

                    dictionary.Add(id, new Movie(name, description));
                }
            }

            return dictionary;
        }
        public void SaveDatabaseToFile()
        {
            StreamWriter writer;

            using (writer = new StreamWriter(_dbFilePath))
            {
                writer.WriteLine("ID,Name,Description");
                foreach (var entry in MoviesDictionary)
                {
                    writer.WriteLine($"{entry.Key},{entry.Value.Name},{entry.Value.Description}");
                }
            }
        }
    }
}
