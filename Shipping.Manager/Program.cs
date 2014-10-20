using Core.CQRS;
using Shipping.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Domain.CommandHandlers;
using Shipping.Domain.AggregateRoots;
using Infrastructure.FakePersistance;

namespace Shipping.Manager
{
    /// <summary>
    /// TODO: Change to service
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureDependancyGraph();

            // Start processing commands/events 
        }

        /// <summary>
        /// TODO: Use IoC instead of manual wire up.
        /// </summary>
        private static void ConfigureDependancyGraph()
        {
            IBus bus = null;
            var shipmentRepo = new Repository<Shipment>();
            var handler = new ShipmentCommandHandler(shipmentRepo);
            bus.RegisterCommandHandler<CompleteShipment>(handler.Handle);
        }
    }
}
