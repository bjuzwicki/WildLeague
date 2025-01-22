using Microsoft.Extensions.Logging;
using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Tests.Leagues
{
	public class LeagueServiceTest
	{
		private readonly ILeagueService _leagueService = new LeagueService(new TeamService(), new ScheduleService(), new SimulationService(null));

		[Fact]
		public void LeagueServiceInstance_ShouldNotBeNull()
		{
			Assert.NotNull(_leagueService);
		}

		[Fact]
		public void Generate_GenerateNewLeague_LeagueNotNull()
		{
			var leagueName = new LeagueName("Ekstraklasa");
			var numberOfTeams = 20;

			var league = _leagueService.Generate(leagueName, numberOfTeams);

			Assert.NotNull(league);
		}
	}
}
