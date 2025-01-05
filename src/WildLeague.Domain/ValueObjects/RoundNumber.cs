using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.ValueObjects
{
	public class RoundNumber : ValueObject
	{
		public int Value { get; }

		public RoundNumber(int value)
		{
			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
