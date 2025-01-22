using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WildLeague.Application.Interfaces.Services;
using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services
{
	public class SimulationService : BackgroundService, ISimulationService
	{
		private readonly ILogger<SimulationService> _logger;

		public SimulationService(ILogger<SimulationService> logger)
		{
			_logger = logger;
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await Task.Delay(1000, stoppingToken);
			}
		}

		public async Task SimulateAsync(League league)
		{
			for (int i = 10; i > 0; i--)
			{
				_logger.LogInformation($"Simulation start in {i}");
				await Task.Delay(1000);
			}

			foreach (var match in league.Schedule.Matches)
			{
				var delay = match.Date - DateTime.Now;
				if (delay > TimeSpan.Zero)
				{
					await Task.Delay(delay);
				}

				var result = await SimulateMatchAsync(match);
				//add result to database? or cache?
			}
		}

		private async Task<MatchResult> SimulateMatchAsync(Match match)
		{
			_logger.LogInformation($"Start match: {match.ToString()}");

			var teamList = new List<Team>()
			{
				match.HomeTeam,
				match.AwayTeam
			};

			int homeTeamGoals = 0;
			int awayTeamGoals = 0;

			_logger.LogInformation($"Start of first half");

			for (int i = 1; i <= 45; i++)
			{
				if(i % 3 == 0)
				{
					var teamInPossession = DeterminePossession(teamList);
					if(AttemptGoal(teamInPossession))
					{
						_logger.LogInformation($"{teamInPossession.Name.Value} scores!");

						if(teamInPossession == match.HomeTeam)
						{
							homeTeamGoals++;
						}
						else if(teamInPossession == match.AwayTeam)
						{
							awayTeamGoals++;
						}
					}
				}

				await Task.Delay(1000);
			}

			_logger.LogInformation($"End of first half");

			await Task.Delay(5000);

			_logger.LogInformation($"Start of second half");

			for (int i = 46; i <= 90; i++)
			{
				if (i % 3 == 0)
				{
					var teamInPossession = DeterminePossession(teamList);
					if (AttemptGoal(teamInPossession))
					{
						_logger.LogInformation($"{teamInPossession.Name.Value} scores!");

						if (teamInPossession == match.HomeTeam)
						{
							homeTeamGoals++;
						}
						else if (teamInPossession == match.AwayTeam)
						{
							awayTeamGoals++;
						}
					}
				}

				await Task.Delay(1000);
			}

			_logger.LogInformation($"End of second half");

			var result = new MatchResult(match, homeTeamGoals, awayTeamGoals);

			_logger.LogInformation($"End match. Result: {result.ToString()}");

			return result;
		}

		private Team DeterminePossession(List<Team> teams)
		{
			Random random = new Random();
			int totalSkill = teams.Sum(team => team.Skill.Value);

			int roll = random.Next(1, totalSkill + 1);
			int cumulativeSkill = 0;

			foreach (var team in teams)
			{
				cumulativeSkill += team.Skill.Value;
				if (roll <= cumulativeSkill)
				{
					return team;
				}
			}

			return teams.Last();
		}

		private bool AttemptGoal(Team team)
		{
			Random random = new Random();

			int chance = random.Next(1, 101);
			return chance <= team.Skill.Value / 4; 
		}
	}
}
