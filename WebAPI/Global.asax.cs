using Infrastructure.MassTransitBusAdapter;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Warehouse.Messages.Events;
using Warehouse.ReadModels.Views;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        // TODO: Move to config file
        private const uint NUM_THREADS = 2;
        private const string DOMAIN = "WebAPI";
        private const string SERVER_NAME = "localhost";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var bus = new MassTransitBusAdapter(SERVER_NAME, DOMAIN, NUM_THREADS);
            BusFactory.Setup(bus);

            var detail = new InventoryItemDetailView();
            bus.RegisterEventSubscriber<InventoryItemCreated>(detail.Handle);
            bus.RegisterEventSubscriber<InventoryItemDeactivated>(detail.Handle);
            bus.RegisterEventSubscriber<InventoryItemRenamed>(detail.Handle);
            bus.RegisterEventSubscriber<ItemsCheckedInToInventory>(detail.Handle);
            bus.RegisterEventSubscriber<ItemsRemovedFromInventory>(detail.Handle);

            var list = new InventoryListView();
            bus.RegisterEventSubscriber<InventoryItemCreated>(list.Handle);
            bus.RegisterEventSubscriber<InventoryItemRenamed>(list.Handle);
            bus.RegisterEventSubscriber<InventoryItemDeactivated>(list.Handle);
        }
    }
}
