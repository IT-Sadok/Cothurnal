using RaceTournament;
using DataRace;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

class Program
{
    static async Task Main(string[] args)
    {
        var tournament = new TournamentResult(Racer.GetRecers());

        tournament.OnRaceCompleted = (racer1, racer2, winner) =>
        {
            Console.WriteLine($"{racer1.Name} vs {racer2.Name}: Winner is {winner.Name}");
            
        };

        await tournament.RunTournamentAsync();

        if (tournament.GetWinner() != null)
        {
            var winner = tournament.GetWinner();
            Console.WriteLine($"Tournament Winner is {winner.Name}");
        }
    }
}