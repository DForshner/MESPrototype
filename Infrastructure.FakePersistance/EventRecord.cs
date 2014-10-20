using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FakePersistance
{
    public class EventRecord
    {
        public readonly Event EventData;
        public readonly Guid Id;
        public readonly int Version;

        public EventRecord(Event eventData, Guid id, int version)
        {
            this.EventData = eventData;
            this.Id = id;
            this.Version = version;
        }
    }
}
