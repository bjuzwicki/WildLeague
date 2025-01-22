using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services.Parameters
{
	public class FirstHalfOfSeasonGenerationParameters
	{
		public IEnumerable<Team> Teams { get; }
		public DateTime StartDate { get; }
		public TimeSpan TimeBetweenRounds { get; }
		public TimeSpan TimeBetweenMatches { get; }

		public FirstHalfOfSeasonGenerationParameters(List<Team> teams, DateTime startDate, TimeSpan timeBetweenRounds, TimeSpan timeBetweenMatches)
		{
			Teams = teams;
			StartDate = startDate;
			TimeBetweenRounds = timeBetweenRounds;
			TimeBetweenMatches = timeBetweenMatches; 
		}
	}
}
