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
            PrintRaceResult(racerModels);
        };

        await tournament.RunTournamentAsync();

        if (tournament.GetWinner() != null)
        {
            var winner = tournament.GetWinner();
            Console.WriteLine($"Tournament Winner is {winner.Name}");
        }
    }

    public static void PrintRaceResult(RacersModel racerModels)
    {
        Console.WriteLine($"{racerModels.firstRacer.Name} vs {racerModels.secondRacer.Name}: Winner is {racerModels.winner.Name}");
    }
}