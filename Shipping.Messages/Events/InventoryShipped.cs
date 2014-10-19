using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Messages.Events
{
    public class InventoryShipped : Event
    {
        public readonly Guid ShipmentId;

        public InventoryShipped(Guid shipmentId)
        {
            this.ShipmentId = shipmentId;
        }
    }
}