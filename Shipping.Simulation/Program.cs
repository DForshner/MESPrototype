using Core.CQRS;
using Infrastructure.MassTransitBusAdapter;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simulating shipping activity...");

            Console.ReadKey();
        }
    }
}
