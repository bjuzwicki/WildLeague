using WildLeague.Domain.Entities;

namespace WildLeague.Application.Interfaces.Services
{
	public interface ISimulationService
	{
		Task SimulateAsync(League league);
	}
}