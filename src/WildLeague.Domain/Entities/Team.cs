using WildLeague.Domain.Abstraction;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Domain.Entities
{
	public class Team : Entity
	{
		public TeamName Name { get; }

		public Team(TeamName name)
		{
			Name = name;
		}	
	}
}
