using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WildLeague.Domain.Abstraction;
using WildLeague.Infrastructure.Persistence.DbContexts;

namespace WildLeague.Infrastructure.Persistence.Extensions
{
	public static class MediatorExtension
	{
		public static async Task DispatchDomainEventsAsync(this IMediator mediator, 
			WildLeagueDbContext dbContext,
			CancellationToken cancellationToken = default)
		{
			var domainEvents = dbContext.ChangeTracker.Entries<Entity>()
				.Select(x => x.Entity)
				.Where(x => x.DomainEvents.Any())
				.SelectMany(x => x.DomainEvents);

			foreach (var domainEvent in domainEvents)
			{
				await mediator.Publish(domainEvent, cancellationToken);
			}
		}
	}
}
