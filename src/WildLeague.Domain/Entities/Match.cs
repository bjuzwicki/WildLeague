using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.Entities
{
	public class Match : Entity
	{
		public Team HomeTeam { get; }
		public Team AwayTeam { get; }
		public DateTime MatchDate { get; private set; }

		public Match(Team homeTeam, Team awayTeam) 
		{
			HomeTeam = homeTeam;
			AwayTeam = awayTeam;
		}

		public Match(Team homeTeam, Team awayTeam, DateTime matchDate)
		{
			HomeTeam = homeTeam;
			AwayTeam = awayTeam;
			MatchDate = matchDate;
		}

		public void SetMatchDate(DateTime matchDate) 
		{
			MatchDate = matchDate;
		}

		public override string ToString()
		{
			return HomeTeam.Name.Value + " (" + MatchDate.ToString() + ") " + AwayTeam.Name.Value;
		}
	}
}
