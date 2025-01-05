using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.ValueObjects
{
	public class LeagueName : ValueObject
	{
		public string Value { get; }

		public LeagueName(string value)
		{
			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
