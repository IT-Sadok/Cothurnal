using Newtonsoft.Json;

namespace DataMovie
{
    public class MovieVaultJson : IDataRepository
    {
        private string _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "movies.json");

        public MovieVaultJson()
        {
            MoviesDictionary = new Dictionary<int, Movie>(GetMovies());
        }
        private Dictionary<int, Movie> MoviesDictionary { get; set; }

        public void SaveMovie(int id, Movie movie)
        {
            MoviesDictionary[id] = movie;
            SaveToFile();
        }

        public void DeleteMovie(int id)
        {
            MoviesDictionary.Remove(id);
            SaveToFile();
        }

        public void UpdateMovie(int id, string newDescription)
        {
            MoviesDictionary[id].Description = newDescription;
            SaveToFile();
        }

        public IReadOnlyDictionary<int, Movie> GetMovies()
        {
            if (!File.Exists(_dbFilePath))
                return new Dictionary<int, Movie>();

            var jsonString = File.ReadAllText(_dbFilePath);

            if (!string.IsNullOrWhiteSpace(jsonString))
                return new Dictionary<int, Movie>();

            return JsonConvert.DeserializeObject<IReadOnlyDictionary<int, Movie>>(jsonString);
        }

        private void SaveToFile()
        {
            var jsonString = JsonConvert.SerializeObject(MoviesDictionary);

            File.WriteAllText(_dbFilePath, jsonString);
        }
    }
}
