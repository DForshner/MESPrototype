using Core.CQRS;
using Core.Messages.Events;
using Infrastructure.FakePersistance;
using Infrastructure.MassTransitBusAdapter;
using Shipping.Domain.AggregateRoots;
using Shipping.Domain.CommandHandlers;
using Shipping.Messages.Commands;

namespace Shipping.Manager
{
    /// <summary>
    /// TODO: Change from console application to service
    /// TODO: Use IoC instead of manual wire up.
    /// </summary>
    public class Program
    {
        // TODO: Move to config file
        private const uint NUM_THREADS = 2;
        private const string DOMAIN = "Shipping";
        private const string SERVER_NAME = "localhost";

        private static IBus _bus;

        static void Main(string[] args)
        {
            _bus = new MassTransitBusAdapter(SERVER_NAME, DOMAIN, NUM_THREADS); 

            // TODO: Don't do this
            AggregateRoot._eventPublisher = _bus;

            ConfigureCommandHandlers();

            // Configure Event Handler

            // Start processing commands/events 
            _bus.Publish(new ManagerStarted(DOMAIN));
        }

        /// <summary>
        /// TODO: Use IoC instead of manual wire up.
        /// </summary>
        private static void ConfigureCommandHandlers()
        {
            var shipmentRepo = new Repository<Shipment>();
            var handler = new ShipmentCommandHandler(shipmentRepo);
            _bus.RegisterCommandHandler<CompleteShipment>(handler.Handle);
        }
    }
}
