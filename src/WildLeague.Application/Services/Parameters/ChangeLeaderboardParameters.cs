using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services.Parameters
{
	public class ChangeLeaderboardParameters
	{
		public Leaderboard Leaderboard { get; }
		public Guid TeamGuid { get; }
		public int PointsToAdd { get; }
		public int GoalsScoredToAdd { get; }
		public int GoalsConcededToAdd { get; }

		public ChangeLeaderboardParameters(Leaderboard leaderboard,
			Guid teamGuid,
			int pointsToAdd,
			int goalsScoredToAdd,
			int goalsConcededToAdd)
		{
			Leaderboard = leaderboard;
			TeamGuid = teamGuid;
			PointsToAdd = pointsToAdd;
			GoalsScoredToAdd = goalsScoredToAdd;
			GoalsConcededToAdd = goalsConcededToAdd;
		}
	}
}
