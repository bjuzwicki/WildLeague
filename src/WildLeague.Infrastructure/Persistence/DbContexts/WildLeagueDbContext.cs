using MediatR;
using Microsoft.EntityFrameworkCore;
using WildLeague.Infrastructure.Persistence.Extensions;

namespace WildLeague.Infrastructure.Persistence.DbContexts
{
	public class WildLeagueDbContext : DbContext
	{
		private readonly IMediator _mediator;

		public WildLeagueDbContext(IMediator mediator)
		{
			_mediator = mediator;
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var result = await base.SaveChangesAsync(cancellationToken);

			await _mediator.DispatchDomainEventsAsync(this, cancellationToken);

			return result;
		}
	}
}
