using DataRace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManagement
{
    public class RacersModel
    {
        public RacersModel(Racer firstRacer, Racer secondRacer, Racer winner)
        {
            FirstRacer = firstRacer;
            SecondRacer = secondRacer;
            Winner = winner;
        }
        public Racer FirstRacer { get; set; }
        public Racer SecondRacer { get; set; }
        public Racer Winner { get; set; }
    }
}
