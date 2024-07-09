using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRacer
{
    internal class RaceTrack
    {
        private int Number {  get; set; }
        private bool IsFree { get; set; }

        public int GetNumber()
        {
            return Number; 
        }
        public void TakeRace()
        {
            IsFree = false;
        }
        
        public void ExemptRace()
        {
            IsFree = true;
        }
    }
}
