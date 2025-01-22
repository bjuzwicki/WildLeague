using System.Text;
using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.Entities
{
	public class Leaderboard : Entity
	{
		private List<LeaderboardEntry> _teams { get;set; }

		public IReadOnlyCollection<LeaderboardEntry> Table => _teams
				.OrderByDescending(x => x.Points)
				.ThenByDescending(y => y.GoalsDifferent)
				.ToList();

		public Leaderboard(List<Team> teams)
		{ 
			_teams = new List<LeaderboardEntry>(teams.Select(x => new LeaderboardEntry(x)));
		}
		public LeaderboardEntry? GetEntryByTeamGuid(Guid teamGuid)
		{
			return _teams.FirstOrDefault(x => x.Team.Guid == teamGuid);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("[Leaderboard]");
			sb.AppendLine("Team | GS | GC | GD | M | Points");

            foreach (var entry in Table)
            {
                sb.AppendLine(entry.ToString());
            }

			return sb.ToString();
        }
	}
}
