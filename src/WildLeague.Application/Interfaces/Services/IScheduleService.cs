using WildLeague.Application.Services.Parameters;
using WildLeague.Domain.Entities;

namespace WildLeague.Application.Interfaces.Services
{
    public interface IScheduleService
    {
        Schedule Generate(ScheduleGenerationParameters parameters);
    }
}
