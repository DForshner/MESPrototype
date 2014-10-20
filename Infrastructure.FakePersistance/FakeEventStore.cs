using Core.CQRS;
using Core.CQRS.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FakePersistance
{
    public class FakeEventStore : IEventStore
    {
        private readonly IPublishEvents _publisher;
        private readonly Dictionary<Guid, List<EventRecord>> _current = new Dictionary<Guid, List<EventRecord>>();

        public FakeEventStore(IPublishEvents publisher)
        {
            this._publisher = publisher;
        }

        public void Save(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            List<EventRecord> records;
            if (!_current.TryGetValue(aggregateId, out records))
            {
                // If first time saving aggregate Id create and empty list
                records = new List<EventRecord>();
                _current.Add(aggregateId, records);
            }

            if (records[records.Count - 1].Version != expectedVersion && expectedVersion != -1)
                throw new ConcurrencyException();

            var i = expectedVersion;
            foreach (var e in events)
            {
                i++;
                e.Version = i;
                records.Add(new EventRecord(e, aggregateId, i));
                
                // Now that events have been persisted publish them.
                _publisher.Publish(e);
            }
        }

        public IEnumerable<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventRecord> records;
            if (!_current.TryGetValue(aggregateId, out records))
                throw new AggregateNotFoundException();

            return records.Select(x => x.EventData).ToList();
        }
    }
}
