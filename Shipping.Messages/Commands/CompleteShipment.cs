using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Messages.Commands
{
    public class CompleteShipment : Command
    {
        public readonly Guid ShipmentId;
        public readonly int OriginalVersion;

        public CompleteShipment(Guid shipmentId, int originalVersion)
        {
            this.ShipmentId = shipmentId;
            this.OriginalVersion = originalVersion;
        }
    }
}
