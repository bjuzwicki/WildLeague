namespace WildLeague.Domain.Abstraction
{
	public abstract class ValueObject
	{
		protected abstract IEnumerable<object> GetEqualityComponents();

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(this, obj)) return true;

			if (obj == null || obj is not ValueObject) return false;

			if (GetType() != obj.GetType()) return false;

			var valueObject = (ValueObject)obj;

			return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetEqualityComponents());
		}

		public static bool operator ==(ValueObject a, ValueObject b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
				return true;

			if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(ValueObject a, ValueObject b)
		{
			return !(a == b);
		}
	}
}
