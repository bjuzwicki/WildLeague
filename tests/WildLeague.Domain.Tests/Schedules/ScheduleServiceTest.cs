using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Tests.Schedules
{
	public class ScheduleServiceTest
	{
		private readonly IScheduleService _scheduleService = new ScheduleService();

		private readonly List<Team> EvenTeamList = new List<Team>()
		{
			new Team(new TeamName("A")),
			new Team(new TeamName("B")),
			new Team(new TeamName("C")),
			new Team(new TeamName("D")),
			//new Team(new TeamName("E")),
			//new Team(new TeamName("F")),
			//new Team(new TeamName("G")),
			//new Team(new TeamName("H")),
			//new Team(new TeamName("J")),
			//new Team(new TeamName("J")),
			//new Team(new TeamName("K")),
			//new Team(new TeamName("L")),
			//new Team(new TeamName("M")),
			//new Team(new TeamName("N")),
			//new Team(new TeamName("O"))
		};

		private readonly List<Team> OddTeamList = new List<Team>()
		{
			new Team(new TeamName("A")),
			new Team(new TeamName("B")),
			new Team(new TeamName("C")),
			new Team(new TeamName("D")),
			new Team(new TeamName("E")),
			//new Team(new TeamName("F")),
			//new Team(new TeamName("G")),
			//new Team(new TeamName("H")),
			//new Team(new TeamName("J")),
			//new Team(new TeamName("J")),
			//new Team(new TeamName("K")),
			//new Team(new TeamName("L")),
			//new Team(new TeamName("M")),
			//new Team(new TeamName("N")),
			//new Team(new TeamName("O"))
		};

		[Fact]
		public void ScheduleServiceInstance_ShouldNotBeNull()
		{
			Assert.NotNull(_scheduleService);
		}

		[Fact]
		public void Create_EmptyTeamList_ShouldThrowException()
		{
			Assert.Throws<Exception>(() => _scheduleService.Create(new List<Team>()));
		}

		[Fact]
		public void Create_ValidEvenTeamList_ReturnsCorrectNumberOfMatches()
		{
			//arrange
			var teamList = EvenTeamList;
			var expectedMatchesCount = teamList.Count * (teamList.Count - 1);

			//act
			var schedule = _scheduleService.Create(teamList);

			//assert
			Assert.True(schedule != null && schedule.Matches.Count == expectedMatchesCount);
		}

		[Fact]
		public void Create_ValidOddTeamList_ReturnsCorrectNumberOfMatches()
		{
			//arrange
			var teamList = OddTeamList;
			var expectedMatchesCount = teamList.Count * (teamList.Count - 1);

			//act
			var schedule = _scheduleService.Create(teamList);

			//assert
			Assert.True(schedule != null && schedule.Matches.Count == expectedMatchesCount);
		}

		[Fact]
		public void Create_ValidEvenTeamList_HomeAndAwayMatchNotInTheSameTime()
		{
			//arrange
			var teamList = EvenTeamList;
			var homeTeam = teamList[0];
			var awayTeam = teamList[1];

			//act
			var schedule = _scheduleService.Create(teamList);

			var homeMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == homeTeam && x.AwayTeam == awayTeam);
			var awayMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == awayTeam && x.AwayTeam == homeTeam);

			//assert
			Assert.True(schedule != null && homeMatch != null && awayMatch != null && homeMatch.MatchDate != awayMatch.MatchDate);
		}

		[Fact]
		public void Create_ValidOddTeamList_HomeAndAwayMatchNotInTheSameTime()
		{
			//arrange
			var teamList = OddTeamList;
			var homeTeam = teamList[0];
			var awayTeam = teamList[1];

			//act
			var schedule = _scheduleService.Create(teamList);

			var homeMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == homeTeam && x.AwayTeam == awayTeam);
			var awayMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == awayTeam && x.AwayTeam == homeTeam);

			//assert
			Assert.True(schedule != null && homeMatch != null && awayMatch != null && homeMatch.MatchDate != awayMatch.MatchDate);
		}
	}
}
