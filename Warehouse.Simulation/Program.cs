using Core.CQRS;
using Infrastructure.MassTransitBusAdapter;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Warehouse.Messages.Commands;
using Warehouse.Messages.Events;

namespace Warehouse.Simulation
{
    public class Program
    {
        // TODO: Move to config file
        private const uint NUM_THREADS = 2;
        private const string DOMAIN = "WarehouseSimulator";
        private const string SERVER_NAME = "localhost";

        private static IBus _bus;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulating warehouse activity...");

            _bus = new MassTransitBusAdapter(SERVER_NAME, DOMAIN, NUM_THREADS);

            const int numberToCreate = 10;

            _bus.RegisterEventSubscriber<LocationCreated>((loc) => Console.WriteLine("Location {0} created.", loc.Name));
            _bus.RegisterEventSubscriber<InventoryDefinitionCreated>((def) => Console.WriteLine("Inventory definition {0} created.", def.Name));
            _bus.RegisterEventSubscriber<InventoryCreated>((inv) => Console.WriteLine("Inventory created."));

            // TODO: wait for events has to happen after locations and item definitions are created 
            var locationCreated = _bus.AsObservable<LocationCreated>()
                .Scan(0, (acc, cur) => ++acc)
                .Where(x => x == numberToCreate);

            var itemDefsCreated = _bus.AsObservable<InventoryDefinitionCreated>()
                .Scan(0, (acc, cur) => ++acc)
                .Where(x => x == numberToCreate);

            locationCreated.Subscribe((x) => Console.WriteLine("All Locations created."));
            itemDefsCreated.Subscribe((x) => Console.WriteLine("All item definitions created."));

            var createLocations = Enumerable.Range(0, numberToCreate)
                .Select(x => new CreateLocation(new Guid(), "Location" + x))
                .SendTo(_bus);

            var createItemDefs = Enumerable.Range(0, numberToCreate)
                .Select(x => new CreateInventoryDefinition(new Guid(), "Item" + x))
                .SendTo(_bus);

            var warehouseCreated = locationCreated.And(itemDefsCreated).Then((x,y) => x);
            var done = Observable.When(warehouseCreated);
            done.Subscribe((created) => 
                { 
                    Console.WriteLine("Creating inventory at locations.");
                    var inventory = createLocations.Zip(createItemDefs, (loc, def) => new { loc.LocationId, def.InventoryDefinitionId }) //.Select(loc => loc.LocationId).Zip(createItemDefs.Select(def => def.InventoryDefinitionId)
                        .Select(x => new CreateInventory(x.LocationId, x.InventoryDefinitionId, 33))
                        .SendTo(_bus);
                });

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}