using WildLeague.Domain.Entities;
using WildLeague.Domain.ValueObjects;

namespace WildLeague.Application.Interfaces.Services
{
    public interface ILeagueService
    {
        League Create(LeagueName leagueName, int numberOfTeams);
        League Create(LeagueName leagueName, List<Team> teams);
    }
}
