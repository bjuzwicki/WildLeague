using WildLeague.Domain.Entities;

namespace WildLeague.Application.Interfaces.Services
{
    public interface IScheduleService
    {
        Schedule Create(IEnumerable<Team> teams, DateTime startDate = default, TimeSpan timeBetweenRounds = default, TimeSpan timeBetweenMatches = default);
    }
}
