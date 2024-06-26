using Newtonsoft.Json;

namespace DataMovie
{
    public class MovieVaultJson : IDataMovieSave
    {
        private string _dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "movies.json");

        public MovieVaultJson()
        {
            DictOfMoviesJson = ReadMovieFromDb();
        }
        private Dictionary<int, Movie> DictOfMoviesJson { get; set; }

        public void SaveMovieToDb(int id, Movie movie)
        {
            DictOfMoviesJson[id] = movie;
            var jsonString = JsonConvert.SerializeObject(DictOfMoviesJson);

            File.WriteAllText(_dbFilePath, jsonString);
        }

        public void DeleteMovieFromDb(int id)
        {
            DictOfMoviesJson.Remove(id);
            var jsonString = JsonConvert.SerializeObject(DictOfMoviesJson);

            File.WriteAllText(_dbFilePath, jsonString);
        }

        public void UptadeMovieFromDb(int id, string newDescription)
        {
            DictOfMoviesJson[id].Description = newDescription;
            var jsonString = JsonConvert.SerializeObject(DictOfMoviesJson);

            File.WriteAllText(_dbFilePath, jsonString);
        }

        private Dictionary<int, Movie> ReadMovieFromDb()
        {
            var jsonString = File.ReadAllText(_dbFilePath);

            if (!File.Exists(_dbFilePath) && !string.IsNullOrWhiteSpace(jsonString))
                return new Dictionary<int, Movie>();

            return JsonConvert.DeserializeObject<Dictionary<int, Movie>>(jsonString);
        }
        public IReadOnlyDictionary<int, Movie> ReadOnlyDictOfMoviesJson()
        {
            return ReadMovieFromDb(); 
        }
    }
}
