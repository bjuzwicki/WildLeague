using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WildLeague.Domain.Abstraction
{
	public abstract class Entity
	{
		[Key]
		public Guid Guid { get; protected set; } = Guid.NewGuid();

		private List<IDomainEvent> _domainEvents = new ();

		[NotMapped]
		public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

		protected void RegisterDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
		internal void ClearDomainEvents() => _domainEvents.Clear();
	}

}
