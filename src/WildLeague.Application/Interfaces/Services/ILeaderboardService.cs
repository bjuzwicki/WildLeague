using WildLeague.Domain.Entities;

namespace WildLeague.Application.Interfaces.Services
{
	public interface ILeaderboardService
	{
		void UpdateLeaderboard(Leaderboard leaderboard, MatchResult matchResult);
	}
}
