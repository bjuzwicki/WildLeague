using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.Entities
{
	public class MatchResult : Entity
	{
		public Match Match { get; }
		public Team HomeTeam => Match.HomeTeam;
		public Team AwayTeam => Match.AwayTeam;
		public int HomeTeamGoals { get; } 
		public int AwayTeamGoals { get; }

		public MatchResult(Match match, int homeTeamGoals, int awayTeamGoals)
		{
			Match = match ?? throw new ArgumentNullException(nameof(match));
			HomeTeamGoals = homeTeamGoals;
			AwayTeamGoals = awayTeamGoals;
		}

		public MatchResults Result =>
			HomeTeamGoals > AwayTeamGoals ? MatchResults.HomeTeamWin :
			(HomeTeamGoals < AwayTeamGoals ? MatchResults.AwayTeamWin : MatchResults.Draw);

		public override string ToString()
		{
			return $"{Match.Date}: {Match.HomeTeam.Name.Value} {HomeTeamGoals} - {AwayTeamGoals} {Match.AwayTeam.Name.Value}";
		}
	}

	public enum MatchResults
	{
		HomeTeamWin,  
		AwayTeamWin, 
		Draw 
	}
}