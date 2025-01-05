using System.Text.RegularExpressions;
using WildLeague.Application.Interfaces.Services;
using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;
using Match = WildLeague.Domain.Entities.Match;

namespace WildLeague.Application.Services
{
    public class ScheduleService : IScheduleService
	{
		public ScheduleService()
		{ 
		}

		public Schedule Create(IEnumerable<Team> teams, DateTime startDate = default, TimeSpan timeBetweenRounds = default, TimeSpan timeBetweenMatches = default)
		{
			if(teams == null || !teams.Any())
			{
				throw new Exception();
			}

			if(startDate == default)
			{
				startDate = DateTime.Now;
			}

            if (timeBetweenRounds == default)
            {
                timeBetweenRounds = new TimeSpan(1, 0, 0); // 1h
            }

            if (timeBetweenMatches == default)
			{
				timeBetweenMatches = new TimeSpan(1, 0, 0); // 1h
			}

			var oddNumberOfTeams = teams.Count() % 2 != 0;

			if(oddNumberOfTeams)
			{
				return CreateForOddNumberOfTeams(teams, startDate, timeBetweenRounds, timeBetweenMatches);
			}
			
			return CreateForEvenNumberOfTeams(teams, startDate, timeBetweenRounds, timeBetweenMatches);
		}

		// parzyste
		private Schedule CreateForEvenNumberOfTeams(IEnumerable<Team> teams, DateTime startDate, TimeSpan timeBetweenRounds, TimeSpan timeBetweenMatches)
		{
			var matchArray = GetMatches(teams);
			Random.Shared.Shuffle(matchArray);

			var teamsCount = teams.Count();
			var roundCount = (teamsCount - 1) * 2;
			var matchesInOneRoundCount = teamsCount / 2;

			var rounds = new List<Round>();
			var scheduledMatches = new List<Match>();
			var date = startDate;

			for (int i = 1; i <= roundCount; i++)
			{
				var matchesInRound = new List<Match>();

				foreach (var match in matchArray.ToList())
				{
					if (matchesInRound.Any(x =>
						x.HomeTeam == match.HomeTeam || x.AwayTeam == match.HomeTeam
						|| x.HomeTeam == match.AwayTeam || x.AwayTeam == match.AwayTeam)
						|| scheduledMatches.Contains(match))
					{
						continue;
					}

					match.SetMatchDate(date);
					date = date.Add(timeBetweenMatches);

					matchesInRound.Add(match);
					scheduledMatches.Add(match);

					if(matchesInRound.Count == matchesInOneRoundCount)
					{
						break;
					}
				}

				date = date.Add(timeBetweenRounds);

				var round = new Round(new RoundNumber(i), matchesInRound);
				rounds.Add(round);
			}

			return new Schedule(rounds);
		}

		// nieparzyste
		private Schedule CreateForOddNumberOfTeams(IEnumerable<Team> teams, DateTime startDate, TimeSpan timeBetweenRounds, TimeSpan timeBetweenMatches)
		{
			var matchArray = GetMatches(teams);
			Random.Shared.Shuffle(matchArray);
			
			var teamsCount = teams.Count();
			var roundCount =  teamsCount * 2;
			var matchesInOneRoundCount = (teamsCount - 1) / 2;

			var rounds = new List<Round>();
			var scheduledMatches = new List<Match>();
			var date = startDate;

			Team? pausingTeam = null;

			for (int i = 1; i <= roundCount; i++)
			{
				var matchesInRound = new List<Match>();

				if (pausingTeam != null)
				{
					var match = matchArray.First(x => x.HomeTeam == pausingTeam || x.AwayTeam == pausingTeam);
					match.SetMatchDate(date);
					date = date.Add(timeBetweenMatches);

					matchesInRound.Add(match);
					scheduledMatches.Add(match);

					pausingTeam = null;
				}

				foreach (var match in matchArray.ToList())
				{
					if (matchesInRound.Any(x =>
						x.HomeTeam == match.HomeTeam || x.AwayTeam == match.HomeTeam
						|| x.HomeTeam == match.AwayTeam || x.AwayTeam == match.AwayTeam)
						|| scheduledMatches.Contains(match))
					{
						continue;
					}

					match.SetMatchDate(date);
					date = date.Add(timeBetweenMatches);

					matchesInRound.Add(match);
					scheduledMatches.Add(match);

					if(matchesInRound.Count == matchesInOneRoundCount)
					{
						var teamsInRound = matchesInRound
							.SelectMany(x => new[] {x.HomeTeam, x.AwayTeam})
							.Distinct()
							.ToList();

						pausingTeam = teams.Except(teamsInRound).First();
						break;
					}
				}

				date = date.Add(timeBetweenRounds);

				var round = new Round(new RoundNumber(i), matchesInRound);
				rounds.Add(round);
			}

			return new Schedule(rounds);
		}

		private Match[] GetMatches(IEnumerable<Team> teamArray)
		{
			var matchList = new List<Match>();

			foreach (var homeTeam in teamArray)
			{
				foreach (var awayTeam in teamArray)
				{
					if (homeTeam == awayTeam)
					{
						continue;
					}

					var match = new Match(homeTeam, awayTeam);
					matchList.Add(match);
				}
			}

			var matchArray = matchList.ToArray();

			return matchArray;
		}
	}
}
