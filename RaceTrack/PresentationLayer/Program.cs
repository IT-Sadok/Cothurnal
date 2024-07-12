using TournamentManagement;
using DataRace;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

class Program
{
    static async Task Main(string[] args)
    {
        var tournament = new TournamentResult(new RacerFactory().GetRecers());

        tournament.OnRaceCompleted = (racerModels) =>
        {
            Console.WriteLine($"{racerModels.FirstRacer.Name} vs {racerModels.SecondRacer.Name}: Winner is {racerModels.Winner.Name}");
        };

        await tournament.RunTournamentAsync();

        if (tournament.GetWinner() != null)
        {
            var winner = tournament.GetWinner();
            Console.WriteLine($"Tournament Winner is {winner.Name}");
        }
    }
}