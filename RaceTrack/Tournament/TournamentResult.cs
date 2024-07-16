using DataRace;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TournamentManagement
{
    public class TournamentResult
    {
        private ConcurrentQueue<Racer> _racers;
        private ConcurrentQueue<Racer> _winners;
        private List<Task> tasks;
        public Action<RacersModel> OnRaceCompleted;

        public TournamentResult(List<Racer> racers)
        {
            _racers = new ConcurrentQueue<Racer>(racers);
            _winners = new ConcurrentQueue<Racer>();
            tasks = new List<Task>();
        }

        private async Task RaceAsync(Racer racer1, Racer racer2)
        {
            var winner = await GetWinnerAsync(racer1, racer2);
            OnRaceCompleted(new RacersModel(racer1,racer2,winner));
            _winners.Enqueue(winner);
        }

        private async Task<Racer> GetWinnerAsync(Racer racer1, Racer racer2)
        {
            await Task.Delay(1000);

            return racer1.Velocity > racer2.Velocity ? racer1 : racer2;
        }

        public async Task RunTournamentAsync()
        {
            while (_racers.Count > 1)
            {
                _racers.TryDequeue(out Racer firstRacer);
                _racers.TryDequeue(out Racer secondRacer);

                tasks.Add(RaceAsync(firstRacer, secondRacer));

                if (tasks.Count >= 5)
                {
                    await Task.WhenAny(tasks);
                }

                await ReloadRacersAsync();
            }
        }

        public async Task ReloadRacersAsync()
        {
            if (_racers.Count == 0)
            {
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