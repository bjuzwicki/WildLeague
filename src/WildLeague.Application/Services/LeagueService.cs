using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services.Parameters;
using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Application.Services
{
    public class LeagueService : ILeagueService
	{
		private readonly ITeamService _teamService;
		private readonly IScheduleService _scheduleService;
		private readonly ISimulationService _simulationService;

		public LeagueService
			(ITeamService teamService, 
			IScheduleService scheduleService,
			ISimulationService simulationService)
		{
			_teamService = teamService;
			_scheduleService = scheduleService;
			_simulationService = simulationService;	
		}

		public League Generate(LeagueName leagueName, int numberOfTeams)
		{
			var teams = _teamService.CreateRandomTeams(numberOfTeams);
			return Generate(leagueName, teams);
		}

		public League Generate(LeagueName leagueName, List<Team> teams)
		{
			var season = new Season(1);
			var schedule = _scheduleService.Generate(new ScheduleGenerationParameters(teams));

			var league = new League(leagueName, season, teams, schedule);

			//to change - run/stop simulation by endpoint
			_simulationService.SimulateAsync(league);

			return league;
		}
	}
}
