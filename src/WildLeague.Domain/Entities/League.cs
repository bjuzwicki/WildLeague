using System.Text;
using WildLeague.Domain.Abstraction;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Entities
{
	public class League : AggregateRoot
	{
		public LeagueName Name { get; private set; }
		public Season Season { get; private set; }
		public Schedule Schedule { get; private set; }

		private readonly List<Team> _teams = new List<Team>();
		public IReadOnlyCollection<Team> Teams => _teams.AsReadOnly();

		public string AsString => ToString() ?? "";

		public League(LeagueName name, Season season, List<Team> teams, Schedule schedule)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Season = season ?? throw new ArgumentNullException(nameof(season));
			_teams = teams ?? throw new ArgumentNullException(nameof(teams));
			Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
		}

		public override string ToString() 
		{ 
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(Name.Value + " (sezon " + Season.Number + ")");
			sb.AppendLine(" ");
			sb.AppendLine(Schedule.ToString());

			return sb.ToString();
		}
	}
}
