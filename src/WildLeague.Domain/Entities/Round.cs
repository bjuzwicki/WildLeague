using System.Text;
using WildLeague.Domain.Abstraction;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Entities
{
	public class Round : Entity
	{
		public RoundNumber Number { get; }
		public List<Match> MatchList { get; }

		public Round(RoundNumber number, List<Match> matchList)
		{
			Number = number;
			MatchList = matchList;
		}

		public override string ToString() 
		{ 
			StringBuilder sb = new StringBuilder();
			sb.Append($"Round {Number.Value}" + Environment.NewLine);

			foreach(var match in MatchList) 
			{
				sb.AppendLine(match.ToString());
			}

			return sb.ToString();
		}
	}
}
