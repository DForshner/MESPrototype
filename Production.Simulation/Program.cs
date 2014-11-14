using Core.CQRS;
using Infrastructure.MassTransitBusAdapter;
using System;

namespace Production.Simulation
{
    public class Program
    {
        // TODO: Move to config file
        private const uint NUM_THREADS = 2;
        private const string DOMAIN = "Production";
        private const string SERVER_NAME = "localhost";

        static void Main(string[] args)
        {
            Console.WriteLine("Simulating production activity...");

            var _bus = new MassTransitBusAdapter(SERVER_NAME, DOMAIN, NUM_THREADS); 


            Console.ReadKey();
        }
    }
}
