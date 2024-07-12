using DataRace;
using System.Collections.Concurrent;

namespace TournamentManagement
{
    public class TournamentResult
    {
        private ConcurrentQueue<Racer> _racers;
        private ConcurrentQueue<Racer> _winners;
        public Action<RacersModel> OnRaceCompleted;

        public TournamentResult(List<Racer> racers)
        {
            _racers = new ConcurrentQueue<Racer>(racers);
            _winners = new ConcurrentQueue<Racer>();
        }

        public async Task RaceAsync(Racer racer1, Racer racer2)
        {
            var winner = await GetWinnerAsync(racer1, racer2);
            OnRaceCompleted(new RacersModel(racer1, racer2, winner));
            _winners.Enqueue(winner);
        }

        public async Task<Racer> GetWinnerAsync(Racer racer1, Racer racer2)
        {
            await Task.Delay(2000);

            return racer1.Velocity > racer2.Velocity ? racer1 : racer2;
        }

        public async Task RunTournamentAsync()
        {
            var tasks = new List<Task>();

            while (_racers.Count > 1)
            {
                while (_racers.Count > 1)
                {
                    _racers.TryDequeue(out Racer firstRacer);
                    _racers.TryDequeue(out Racer secondRacer);

                    tasks.Add(RaceAsync(firstRacer, secondRacer));

                    if (tasks.Count >= 5)
                    {
                        await Task.WhenAny(tasks);
                        tasks.Clear();
                    }
                }
                await Task.WhenAll(tasks);

                _racers = new ConcurrentQueue<Racer>(_winners);
                _winners.Clear();
            }
        }

        public Racer? GetWinner()
        {
            _racers.TryPeek(out Racer racer);
            return _racers.Count == 1 ? racer : null;
        }
    }
}