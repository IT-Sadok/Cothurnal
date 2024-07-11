using DataRace;

namespace RaceTournament
{
    public class TournamentResult
    {
        private Queue<Racer> _racers;
        public Queue<Racer> _winners = new Queue<Racer>();
        public Action<Racer, Racer, Racer> OnRaceCompleted;

        public TournamentResult(List<Racer> racers)
        {
            _racers = new Queue<Racer>(racers);
        }

        public async Task RaceAsync(Racer racer1, Racer racer2)
        {
            var winner = await GetWinnerAsync(racer1, racer2);
            OnRaceCompleted(racer1, racer2, winner);
            _winners.Enqueue(winner);
        }

        public async Task<Racer> GetWinnerAsync(Racer racer1, Racer racer2)
        {
            await Task.Delay(2000);

            return racer1.Velocity > racer2.Velocity ? racer1 : racer2;
        }

        public async Task RunTournamentAsync()
        {
            while (_racers.Count > 1)
            {
                var tasks = new List<Task>();

                while (_racers.Count > 1)
                {
                    var firstRacer = _racers.Dequeue();
                    var secondRacer = _racers.Dequeue();

                    tasks.Add(RaceAsync(firstRacer, secondRacer));

                    if (tasks.Count >= 5)
                    {
                        await Task.WhenAll(tasks);
                        tasks.Clear();
                    }
                }
                await Task.WhenAll(tasks);

                _racers = new Queue<Racer>(_winners);
                _winners.Clear();
            }
        }

        public Racer GetWinner()
        {
            return _racers.Count == 1 ? _racers.Peek() : null;
        }
    }
}