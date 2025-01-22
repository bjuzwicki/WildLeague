using WildLeague.Application.Interfaces.Services;
using WildLeague.Application.Services.Parameters;
using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;
using Match = WildLeague.Domain.Entities.Match;

namespace WildLeague.Application.Services
{
    public class ScheduleService : IScheduleService
	{
		public Schedule Generate(ScheduleGenerationParameters parameters)
		{
			ValidateParameters(parameters);

			var teams = AdjustTeamsForOddCount(parameters.Teams.ToList());

			var rounds = GenerateRounds(teams, parameters.StartDate, parameters.TimeBetweenRounds, parameters.TimeBetweenMatches);

			return new Schedule(rounds);
		}

		private void ValidateParameters(ScheduleGenerationParameters parameters)
		{
			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if (!parameters.IsValid())
				throw new ArgumentException("Invalid parameters", nameof(parameters));
		}

		private List<Team> AdjustTeamsForOddCount(List<Team> teams)
		{
			if (teams.Count % 2 != 0)
			{
				teams.Add(Team.CreateEmpty());
			}
				
			return teams;
		}

		private List<Round> GenerateRounds(
			List<Team> teams,
			DateTime startDate,
			TimeSpan timeBetweenRounds,
			TimeSpan timeBetweenMatches)
		{
			var firstHalf = GenerateFirstHalfOfSeasonRounds(new FirstHalfOfSeasonGenerationParameters(teams, startDate, timeBetweenRounds, timeBetweenMatches));
			var secondHalf = GenerateSecondHalfOfSeasonRounds(new SecondHalfOfSeasonGenerationParameters(firstHalf, timeBetweenRounds, timeBetweenMatches));

			return firstHalf.Concat(secondHalf).ToList();
		}

		private List<Round> GenerateFirstHalfOfSeasonRounds(FirstHalfOfSeasonGenerationParameters parameters)
		{
			var rounds = new List<Round>();
			var teams = parameters.Teams.ToList();
			var teamCount = teams.Count;
			var totalRounds = teamCount - 1;
			var matchesPerRound = teamCount / 2;

			var date = parameters.StartDate;

			for (int roundNumber = 0; roundNumber < totalRounds; roundNumber++)
			{
				var matchesInRound = new List<Match>();

				for (int match = 0; match < matchesPerRound; match++)
				{
					var home = (roundNumber + match) % (teamCount - 1);
					var away = (teamCount - 1 - match + roundNumber) % (teamCount - 1);

					if (match == 0) away = teamCount - 1;

					if (!teams[home].IsEmpty() && !teams[away].IsEmpty())
					{
						matchesInRound.Add(new Match(teams[home], teams[away], date));
						date = date.Add(parameters.TimeBetweenMatches);
					}
				}

				rounds.Add(new Round(new RoundNumber(roundNumber + 1), matchesInRound));
				date = date.Add(parameters.TimeBetweenRounds);
			}

			return rounds;
		}

		private List<Round> GenerateSecondHalfOfSeasonRounds(SecondHalfOfSeasonGenerationParameters parameters)
		{
			var rematchRounds = new List<Round>();
			var firstHalfOfSeasonRoundsCount = parameters.FirstHalfOfSeasonRounds.Count;

			var shuffledFirstHalf = parameters.FirstHalfOfSeasonRounds.ToArray();
			Random.Shared.Shuffle(shuffledFirstHalf);

			var date = shuffledFirstHalf.Last().MatchList.Last().Date;

			foreach (var round in shuffledFirstHalf)
			{
				var rematches = round.MatchList
					.Select(match => new Match(match.AwayTeam, match.HomeTeam, date))
					.ToList();

				foreach (var rematch in rematches)
				{
					rematch.SetMatchDate(date);
					date = date.Add(parameters.TimeBetweenMatches);
				}
					
				rematchRounds.Add(new Round(new RoundNumber(rematchRounds.Count + 1 + firstHalfOfSeasonRoundsCount), rematches));
				date = date.Add(parameters.TimeBetweenRounds);
			}

			return rematchRounds;
		}
	}
}
