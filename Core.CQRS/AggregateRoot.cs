using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public abstract class AggregateRoot
    {
        public abstract Guid Id { get; }
        public int Version { get; internal set; }

        private IDomainEventPublisher _eventPublisher;

        public AggregateRoot()
        {
            this.Version = -1;
        }

        protected virtual IDomainEventPublisher EventPublisher
        {
            get { return _eventPublisher; }
            set
            {
                if (_eventPublisher != null) 
                    throw new InvalidOperationException("Event publisher already set.");
                _eventPublisher = value;
            }
        }
    }
}
