using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Tests.Leagues
{
	public class LeagueServiceTest
	{
		private readonly ILeagueService _leagueService = new LeagueService(new TeamService(), new ScheduleService());

		[Fact]
		public void Create_CreateNewLeague_LeagueNotNull()
		{
			var leagueName = new LeagueName("Ekstraklasa");
			var numberOfTeams = 15;

			var league = _leagueService.Create(leagueName, numberOfTeams);

			Assert.NotNull(league);
		}
	}
}
