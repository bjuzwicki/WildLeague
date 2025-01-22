using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services.Parameters;
using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services
{
	public class LeaderboardService : ILeaderboardService
	{
		private readonly int _winPoints = 3;
		private readonly int _lossPoints = 0;
		private readonly int _drawPoints = 1;

		public void UpdateLeaderboard(Leaderboard leaderboard, MatchResult matchResult)
		{
			switch (matchResult.Result)
			{
				case MatchResults.HomeTeamWin:
					UpdateLeaderboard(leaderboard, matchResult, _winPoints, _lossPoints);
					break;

				case MatchResults.AwayTeamWin:
					UpdateLeaderboard(leaderboard, matchResult, _lossPoints, _winPoints);
					break;

				case MatchResults.Draw:
					UpdateLeaderboard(leaderboard, matchResult, _drawPoints, _drawPoints);
					break;
			}
		}

		private void UpdateLeaderboard(Leaderboard leaderboard, MatchResult matchResult, int homeTeamPoints, int awayTeamPoints)
		{
			UpdateLeaderboard(new ChangeLeaderboardParameters(
				leaderboard,
				matchResult.HomeTeam.Guid,
				homeTeamPoints,
				matchResult.HomeTeamGoals,
				matchResult.AwayTeamGoals));

			UpdateLeaderboard(new ChangeLeaderboardParameters(
				leaderboard,
				matchResult.AwayTeam.Guid,
				awayTeamPoints,
				matchResult.AwayTeamGoals,
				matchResult.HomeTeamGoals));
		}

		private void UpdateLeaderboard(ChangeLeaderboardParameters parameters)
		{
			var entry = parameters.Leaderboard.GetEntryByTeamGuid(parameters.TeamGuid);

			if (entry == null)
			{
				throw new ArgumentException($"Team with GUID {parameters.TeamGuid} not found.");
			}

			entry.Points += parameters.PointsToAdd;
			entry.GoalsScored += parameters.GoalsScoredToAdd;
			entry.GoalsConceded += parameters.GoalsConcededToAdd;
			entry.MatchesPlayed += 1;
		}
	}
}
