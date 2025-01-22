using WildLeague.Domain.Abstraction;

namespace WildLeague.Domain.ValueObjects
{
	public class TeamSkill : ValueObject
	{
		public int Value { get; set; }

		public TeamSkill(int value)
		{
			Value = value;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
