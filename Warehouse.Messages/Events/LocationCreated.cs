using Core.CQRS;
using System;

namespace Warehouse.Messages.Events
{
    public class LocationCreated : Event
    {
        public readonly Guid Id;
        public readonly string Name;

        public LocationCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
