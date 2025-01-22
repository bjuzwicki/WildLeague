using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.Entities
{
	public class Match : Entity
	{
		public Team HomeTeam { get; }
		public Team AwayTeam { get; }
		public DateTime Date { get; private set; }

		public Match(Team homeTeam, Team awayTeam) 
		{
			HomeTeam = homeTeam;
			AwayTeam = awayTeam;
		}

		public Match(Team homeTeam, Team awayTeam, DateTime date)
		{
			HomeTeam = homeTeam;
			AwayTeam = awayTeam;
			Date = date;
		}

		public void SetMatchDate(DateTime date) 
		{
			Date = date;
		}

		public override string ToString()
		{
			return HomeTeam.Name.Value + " (" + Date.ToString() + ") " + AwayTeam.Name.Value;
		}
	}
}
