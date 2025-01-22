using System.Text;

namespace WildLeague.Domain.Entities
{
	public class LeaderboardEntry
	{
		public Team Team { get; set; }
		public int Points { get; set; }
		public int GoalsScored { get; set; }
		public int GoalsConceded { get; set; }
		public int MatchesPlayed { get; set; }
		public int GoalsDifferent => GoalsScored - GoalsConceded;

		public LeaderboardEntry(Team team)
		{
			Team = team;
			Points = 0;
			GoalsScored = 0;
			GoalsConceded = 0;
			MatchesPlayed = 0;
		}

		public LeaderboardEntry(Team team, int points, int goalsScored, int goalsConceded, int matchesPlayed)
		{
			Team = team;
			Points = points;
			GoalsScored = goalsScored;
			GoalsConceded = goalsConceded;
			MatchesPlayed = matchesPlayed;
		}

		public override string ToString() 
		{
			return $"{Team.Name.Value} {GoalsScored} {GoalsConceded} {GoalsDifferent} {MatchesPlayed} {Points}";
		}
	}
}
