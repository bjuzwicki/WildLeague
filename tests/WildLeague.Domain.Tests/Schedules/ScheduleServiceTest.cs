using System.Diagnostics;
using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services;
using WildLeague.Application.Services.Parameters;
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
			new Team(new TeamName("E")),
			new Team(new TeamName("F")),
			new Team(new TeamName("G")),
			new Team(new TeamName("H")),
			new Team(new TeamName("J")),
			new Team(new TeamName("J")),
			new Team(new TeamName("K")),
			new Team(new TeamName("L")),
			new Team(new TeamName("M")),
			new Team(new TeamName("N")),
		};

		private readonly List<Team> OddTeamList = new List<Team>()
		{
			new Team(new TeamName("A")),
			new Team(new TeamName("B")),
			new Team(new TeamName("C")),
			new Team(new TeamName("D")),
			new Team(new TeamName("E")),
		    new Team(new TeamName("F")),
			new Team(new TeamName("G")),
			new Team(new TeamName("H")),
			new Team(new TeamName("J")),
			new Team(new TeamName("J")),
			new Team(new TeamName("K")),
			new Team(new TeamName("L")),
			new Team(new TeamName("M")),
			new Team(new TeamName("N")),
			new Team(new TeamName("O"))
		};

		[Fact]
		public void ScheduleServiceInstance_ShouldNotBeNull()
		{
			Assert.NotNull(_scheduleService);
		}

		[Fact]
		public void Generate_EmptyTeamList_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() => _scheduleService.Generate(new ScheduleGenerationParameters(new List<Team>())));
		}

		[Fact]
		public void Generate_ValidEvenTeamList_ReturnsCorrectNumberOfMatches()
		{
			//arrange
			var teamList = EvenTeamList;
			var expectedMatchesCount = teamList.Count * (teamList.Count - 1);

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			//assert
			Assert.True(schedule.Matches.Count == expectedMatchesCount);
		}

		[Fact]
		public void Generate_ValidOddTeamList_ReturnsCorrectNumberOfMatches()
		{
			//arrange
			var teamList = OddTeamList;
			var expectedMatchesCount = teamList.Count * (teamList.Count - 1);

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			if(schedule.Matches.Count  != expectedMatchesCount)
			{
				Debug.WriteLine(schedule.ToString());
			}

			//assert
			Assert.True(schedule.Matches.Count == expectedMatchesCount);
		}

		[Fact]
		public void Generate_ValidEvenTeamList_HomeAndAwayMatchNotInTheSameTime()
		{
			//arrange
			var teamList = EvenTeamList;
			var homeTeam = teamList[0];
			var awayTeam = teamList[1];

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			var homeMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == homeTeam && x.AwayTeam == awayTeam);
			var awayMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == awayTeam && x.AwayTeam == homeTeam);

			//assert
			Assert.True(homeMatch != null && awayMatch != null && homeMatch.Date != awayMatch.Date);
		}

		[Fact]
		public void Generate_ValidOddTeamList_HomeAndAwayMatchNotInTheSameTime()
		{
			//arrange
			var teamList = OddTeamList;
			var homeTeam = teamList[0];
			var awayTeam = teamList[1];

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			var homeMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == homeTeam && x.AwayTeam == awayTeam);
			var awayMatch = schedule.Matches.FirstOrDefault(x => x.HomeTeam == awayTeam && x.AwayTeam == homeTeam);

			//assert
			Assert.True(homeMatch != null && awayMatch != null && homeMatch.Date != awayMatch.Date);
		}

		[Fact]
		public void Generate_ValidEvenTeams_TeamHasCorrectMatchCount()
		{
			//arrange
			var teamList = EvenTeamList;
			var team = teamList[0];

			var expectedMatchesForOneTeamCount = (teamList.Count - 1) * 2;

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			var matches = schedule.Matches
				.Where(x => x.HomeTeam == team || x.AwayTeam == team);

			//assert
			Assert.True(matches.Count() == expectedMatchesForOneTeamCount);
		}

		[Fact]
		public void Generate_ValidOddTeams_TeamHasCorrectMatchCount()
		{
			//arrange
			var teamList = OddTeamList;
			var team = teamList[0];

			var expectedMatchesForOneTeamCount = (teamList.Count - 1) * 2;

			//act
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teamList));

			var matches = schedule.Matches
				.Where(x => x.HomeTeam == team || x.AwayTeam == team);

			if(matches.Count() != expectedMatchesForOneTeamCount)
			{
				Debug.WriteLine(schedule.ToString());
			}

			//assert
			Assert.True(matches.Count() == expectedMatchesForOneTeamCount);
		}
	}
}
