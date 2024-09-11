namespace DataAccounts.Repositories.MovieEntitys
{
    public class MovieInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Views { get; set; }
        public string Description { get; set; }
        public List<string> Genres { get; set; }
    }
}
