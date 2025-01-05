using WildLeague.Domain.Entities;

namespace WildLeague.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Team CreateRandomTeam();
        List<Team> CreateRandomTeams(int numberOfTeams);
    }
}
