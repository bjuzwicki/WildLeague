using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Domain.Entities;

namespace WildLeague.Domain.Tests.Teams
{
	public class TeamServiceTest
	{
		private readonly ITeamService _teamService = new TeamService();

		[Fact]
		public void CreateRandomTeam_GenerateOneTeam_GeneratedTeamNotNull()
		{
			Team team = _teamService.CreateRandomTeam();

			Assert.NotNull(team);
		}

		[Fact]
		public void CreateRandomTeams_GenerateSevenTeams_CorrectNumberOfGeneratedTeams()
		{
			var numberOfTeams = 7;

			var teams = _teamService.CreateRandomTeams(7);

			Assert.True(teams.Count()  == numberOfTeams);
		}
	}
}
