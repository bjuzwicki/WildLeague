using WildLeague.Domain.Abstraction;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Entities
{
	public class Team : Entity
	{
		public TeamName Name { get; }

		public TeamSkill Skill { get; set; }

		private readonly bool _isEmpty;

		public Team(TeamName name)
		{
			Name = name;
		}	

		private Team()
		{
			_isEmpty = true;
		}

		public void AddSkill(TeamSkill skill) 
		{
			Skill = skill;
		}

		public static Team CreateEmpty()
		{
			return new Team();
		}

		public bool IsEmpty()
		{
			return _isEmpty;
		}

		public override bool Equals(object obj)
		{
			if (obj is not Team other)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			if (GetType() != other.GetType())
				return false;

			return Guid != Guid.Empty && Guid == other.Guid;
		}

		public override int GetHashCode()
		{
			return Guid.GetHashCode();
		}

		public static bool operator ==(Team left, Team right)
		{
			if (left is null && right is null)
				return true;

			if (left is null || right is null)
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(Team left, Team right)
		{
			return !(left == right);
		}
	}
}
