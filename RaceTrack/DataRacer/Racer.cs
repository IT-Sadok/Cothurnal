
namespace DataRace
{
    public class Racer
    {
        public Racer(string name, int velocity)
        {
            Name = name;
            Velocity = velocity;
        }
        public string Name { get; set; }
        public int Velocity { get; set; }

        public static List<Racer> GetRecers()
        {
            var listOfRacers = new List<Racer>();

            for (int i = 1; i <= 16; i++)
            {
                var racer = new Racer($"Racer{i}", new Random().Next(1, 100));
                listOfRacers.Add(racer);
            }
            return listOfRacers;
        }
    }
}