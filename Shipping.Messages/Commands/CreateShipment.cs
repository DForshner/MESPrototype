using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Messages.Commands
{
    public class CreateShipment : Command 
    {
        public readonly Guid ShipmentId;

        public CreateShipment(Guid id)
        {
            this.ShipmentId = id;
        }
    }
}
