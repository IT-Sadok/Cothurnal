using DataRace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManagement
{
    public class RacerFactory
    {
        public List<Racer> GetRecers()
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
