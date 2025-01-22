using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Application.Interfaces.Services
{
    public interface ILeagueService
    {
		League Generate(LeagueName leagueName, int numberOfTeams);
        League Generate(LeagueName leagueName, List<Team> teams);
    }
}
