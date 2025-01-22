using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Domain.Entities;

namespace WildLeague.Domain.Tests.Teams
{
	public class TeamServiceTest
	{
		private readonly ITeamService _teamService = new TeamService();

		[Fact]
		public void TeamServiceInstance_ShouldNotBeNull()
		{
			Assert.NotNull(_teamService);
		}

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

			var teams = _teamService.CreateRandomTeams(numberOfTeams);

			Assert.True(teams.Count()  == numberOfTeams);
		}

		[Fact]
		public void CreateRandomTeams_Generate20Teams_CorrectNumberOfGeneratedTeams()
		{
			var numberOfTeams = 20;

			var teams = _teamService.CreateRandomTeams(numberOfTeams);

			Assert.True(teams.Count() == numberOfTeams);
		}
	}
}
