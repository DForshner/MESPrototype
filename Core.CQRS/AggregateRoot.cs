using System;

namespace Core.CQRS
{
    public abstract class AggregateRoot
    {
        public abstract Guid Id { get; }
        public int Version { get; internal set; }

        // TODO: Don't do this
        public static IPublishEvents _eventPublisher;

        public AggregateRoot()
        {
            this.Version = -1;
        }

        protected virtual IPublishEvents EventPublisher
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
