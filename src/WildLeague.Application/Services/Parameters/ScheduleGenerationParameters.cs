using WildLeague.Domain.Entities;

namespace WildLeague.Application.Services.Parameters
{
	public class ScheduleGenerationParameters
	{
		public IEnumerable<Team> Teams { get; }
		public DateTime StartDate { get; }
		public TimeSpan TimeBetweenRounds { get; }
		public TimeSpan TimeBetweenMatches { get; }

		public ScheduleGenerationParameters(IEnumerable<Team> teams) 
		{ 
			Teams = teams;

			StartDate = DateTime.Now;

			TimeBetweenRounds = new TimeSpan(0, 1, 0); // 1m

			TimeBetweenMatches = new TimeSpan(0, 1, 0); // 1m
		}

		public ScheduleGenerationParameters(IEnumerable<Team> teams, DateTime startDate, TimeSpan timeBetweenRounds, TimeSpan timeBetweenMatches)
		{
			Teams = teams;

			if (startDate == default)
			{
				startDate = DateTime.Now;
			}

			if (timeBetweenRounds == default)
			{
				timeBetweenRounds = new TimeSpan(0, 1, 0); // 1m
			}

			if (timeBetweenMatches == default)
			{
				timeBetweenMatches = new TimeSpan(0, 1, 0); // 1m
			}

			StartDate = startDate;
			TimeBetweenRounds = timeBetweenRounds;
			TimeBetweenMatches = timeBetweenMatches;
		}

		public bool IsValid()
		{
			return Teams != null && Teams.Any() && TimeBetweenRounds > TimeSpan.Zero && TimeBetweenMatches > TimeSpan.Zero;
		}
	}
}
