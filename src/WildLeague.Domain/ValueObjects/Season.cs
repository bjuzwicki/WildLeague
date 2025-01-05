using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.ValueObjects
{
	public class Season : ValueObject
	{
		public int Number { get; }

		public Season(int number) 
		{
			Number = number;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Number;
		}
	}
}
