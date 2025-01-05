using WildLeague.Application.Interfaces.Services;
using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Application.Services
{
    public class LeagueService : ILeagueService
	{
		private readonly ITeamService _teamService;
		private readonly IScheduleService _scheduleService;

		public LeagueService
			(ITeamService teamService, 
			IScheduleService scheduleService)
		{
			_teamService = teamService;
			_scheduleService = scheduleService;
		}

		public League Create(LeagueName leagueName, int numberOfTeams)
		{
			var teams = _teamService.CreateRandomTeams(numberOfTeams);
			return Create(leagueName, teams);
		}

		public League Create(LeagueName leagueName, List<Team> teams)
		{
			var season = new Season(1);
			var schedule = _scheduleService.Create(teams);

			return new League(leagueName, season, teams, schedule);
		}
	}
}
