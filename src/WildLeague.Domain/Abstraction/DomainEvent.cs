namespace WildLeague.Domain.Abstraction
{
	public class DomainEvent : IDomainEvent
	{
		public DateTime DateOccured { get; protected set; } = DateTime.Now;
	}
}