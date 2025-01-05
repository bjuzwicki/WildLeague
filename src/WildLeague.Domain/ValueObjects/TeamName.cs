using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.ValueObjects
{
	public class TeamName : ValueObject
	{
		public string Value { get; }

		public TeamName(string value) 
		{ 
			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			 yield return Value;
		}
	}
}
