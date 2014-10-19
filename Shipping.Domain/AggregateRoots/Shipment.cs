using Core.CQRS;
using Core.Domain.Exceptions;
using Shipping.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Domain.AggregateRoots
{
    internal class Shipment : AggregateRoot 
    {
        private readonly Guid _id;
        public ShipmentStatues _status;

        public Shipment(Guid id)
        {
            this._id = id;
        }

        public override Guid Id { get { return _id; } }
        public ShipmentStatues Status { get { return _status; } }

        public void Ship()
        {
            if (_status!= ShipmentStatues.Waiting)
                throw new IllegalStateException("Cannot ship in status " + _status);

            _status = ShipmentStatues.Sent;
            EventPublisher.Publish(new InventoryShipped(_id));
        }
    }
}
