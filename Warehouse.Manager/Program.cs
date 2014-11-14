using Core.CQRS;
using Core.Messages.Events;
using Domain.Warehouse.Domain;
using Infrastructure.FakePersistance;
using Infrastructure.MassTransitBusAdapter;
using System;
using Warehouse.Domain.CommandHandlers;
using Warehouse.Messages.Commands;

namespace Warehouse.Manager
{
    /// <summary>
    /// TODO: Change from console application to service
    /// TODO: Use IoC instead of manual wire up.
    /// </summary>
    class Program
    {
        // TODO: Move to config file
        private const uint NUM_THREADS = 2;
        private const string DOMAIN = "WarehouseManager";
        private const string SERVER_NAME = "localhost";

        private static IBus _bus;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting warehouse manager");

            _bus = new MassTransitBusAdapter(SERVER_NAME, DOMAIN, NUM_THREADS); 

            // TODO: Don't do this
            AggregateRoot._eventPublisher = _bus;

            ConfigureCommandHandlers();

            // Configure Event Handler

            // Start processing commands/events 
            _bus.Publish(new ManagerStarted(DOMAIN));

            Console.ReadKey();
            Console.WriteLine("Press any key to exit.");
        }

        private static void ConfigureCommandHandlers()
        {
            var locationRepo = new Repository<Location>();
            var locationHandler = new LocationCommandHandlerLogger(new LocationCommandHandler(locationRepo));
            _bus.RegisterCommandHandler<CreateLocation>(locationHandler.Handle);
            _bus.RegisterCommandHandler<CreateInventoryDefinition>(locationHandler.Handle);

            var inventoryHandler = new InventoryCommandHandler();
            _bus.RegisterCommandHandler<CreateInventory>(inventoryHandler.Handle);
        }

        public class LocationCommandHandlerLogger :
            IHandle<CreateLocation>,
            IHandle<CreateInventoryDefinition>
        {
            private LocationCommandHandler inner;

            public LocationCommandHandlerLogger(LocationCommandHandler inner)
            {
                this.inner = inner;
            }
                
            public void Handle(CreateLocation cmd)
            {
                Console.WriteLine("Creating location {0}", cmd.Name);
                inner.Handle(cmd);
            }

            public void Handle(CreateInventoryDefinition cmd)
            {
                Console.WriteLine("Creating Inventory Definition {0}", cmd.Name);
                inner.Handle(cmd);
            }
        }
    }
}
