using Core.CQRS;
using System;

namespace Warehouse.Messages.Commands
{
    public class CreateLocation : Command
    {
        public readonly Guid LocationId;
        public readonly string Name;

        public CreateLocation(Guid locationId, string name)
        {
            this.LocationId = locationId;
            this.Name = name;
        }
    }
}
