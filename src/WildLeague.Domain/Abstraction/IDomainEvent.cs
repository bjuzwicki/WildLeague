using MediatR;

namespace WildLeague.Domain.Abstraction
{
	public interface IDomainEvent : INotification
	{
		public DateTime DateOccured { get; }
	}
}
