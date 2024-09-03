
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
    }
}