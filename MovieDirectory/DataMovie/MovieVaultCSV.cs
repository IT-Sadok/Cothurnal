using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataMovie
{
    public class MovieVaultCSV : IDataMovieSave
    {
        private string _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "movies.csv");

        public MovieVaultCSV()
        {
            DictOfMoviesCSV = ReadMovieFromDb();
        }
        private Dictionary<int, Movie> DictOfMoviesCSV { get; set; }

        public void DeleteMovieFromDb(int id)
        {
            DictOfMoviesCSV.Remove(id);

            CensuseDatabase();
        }

        public void SaveMovieToDb(int id, Movie movie)
        {
            DictOfMoviesCSV[id] = movie;

            CensuseDatabase();
        }

        public void UptadeMovieFromDb(int id, string newDescription)
        {
            DictOfMoviesCSV[id].Description = newDescription;

            CensuseDatabase();
        }
        public Dictionary<int, Movie> ReadMovieFromDb()
        {
            if (!File.Exists(_dbFilePath))
                return new Dictionary<int, Movie>();

            Dictionary<int, Movie> dictionary = new Dictionary<int, Movie>();

            using (StreamReader reader = new StreamReader(_dbFilePath))
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
        public void CensuseDatabase()
        {
            using (StreamWriter writer = new StreamWriter(_dbFilePath))
            {
                writer.WriteLine("ID,Name,Description");
                foreach (var entry in DictOfMoviesCSV)
                {
                    writer.WriteLine($"{entry.Key},{entry.Value.Name},{entry.Value.Description}");
                }
            }
        }
        public IReadOnlyDictionary<int, Movie> ReadOnlyDictOfMoviesJson()
        {
            return ReadMovieFromDb();
        }
    }
}
