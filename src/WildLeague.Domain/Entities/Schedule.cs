using System.Text;
using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.Entities
{
	public class Schedule : Entity
	{
		public List<Round> Rounds { get; }

		public IReadOnlyCollection<Match> Matches => Rounds.SelectMany(x => x.MatchList).ToList();

		public Schedule(List<Round> rounds)
		{
			Rounds = rounds;
		}

		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Schedule:");

			foreach (var round in Rounds) 
			{
				sb.AppendLine(round.ToString());
			}

			return sb.ToString();
		}
	}
}
