using WildLeague.Application.Interfaces.Services;
using WildLeague.Domain.Entities;
using WildLeague.Domain.Helpers;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Application.Services
{
    public class TeamService : ITeamService
	{
		private readonly Random _random;
		
		public TeamService() 
		{ 
			_random = new Random();
		}

		public Team CreateRandomTeam()
		{
			var randomAdjective = TeamNameData.Adjectives[_random.Next(TeamNameData.Adjectives.Length)];
			var randomNoun = TeamNameData.Nouns[_random.Next(TeamNameData.Nouns.Length)];
			var teamName = $"{randomAdjective} {randomNoun}";

			return new Team(new TeamName(teamName));
		}

		public List<Team> CreateRandomTeams(int numberOfTeams)
		{
			var teams = new List<Team>();

			for (int i = 0; i < numberOfTeams; i++)
			{
				teams.Add(CreateRandomTeam());
			}

			return teams;
		}
	}

}
