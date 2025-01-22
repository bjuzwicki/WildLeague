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
			var skills = GenerateSkills(numberOfTeams, 50, 15);

			for (int i = 0; i < numberOfTeams; i++)
			{
				var team = CreateRandomTeam();
				team.AddSkill(new TeamSkill(skills[i]));

				teams.Add(team);
			}

			return teams;
		}

		private List<int> GenerateSkills(int count, double mean, double stdDev)
		{
			Random random = new Random();
			List<int> skills = new List<int>();

			for (int i = 0; i < count; i++)
			{
				double gaussianValue = GenerateGaussian(random, mean, stdDev);

				int skill = (int)Math.Round(Math.Clamp(gaussianValue, 0, 100));
				skills.Add(skill);
			}

			return skills;
		}

		private double GenerateGaussian(Random random, double mean, double stdDev)
		{
			double u1 = 1.0 - random.NextDouble();
			double u2 = 1.0 - random.NextDouble();
			double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
			double randNormal = mean + stdDev * randStdNormal;

			return randNormal;
		}
	}

}
