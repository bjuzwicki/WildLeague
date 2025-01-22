using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services.Parameters
{
	public class SecondHalfOfSeasonGenerationParameters
	{
		public List<Round> FirstHalfOfSeasonRounds { get; }
		public TimeSpan TimeBetweenRounds { get; }
		public TimeSpan TimeBetweenMatches { get; }

		public SecondHalfOfSeasonGenerationParameters(List<Round> firstHalfOfSeasonRounds, TimeSpan timeBetweenRounds, TimeSpan timeBetweenMatches)
		{
			FirstHalfOfSeasonRounds = firstHalfOfSeasonRounds;
			TimeBetweenRounds = timeBetweenRounds;
			TimeBetweenMatches = timeBetweenMatches;
		}
	}
}
