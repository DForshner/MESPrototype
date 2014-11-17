using Core.CQRS;
using System;

namespace WebAPI 
{
    /// <summary>
    /// TODO: Change to IoC
    /// </summary>
    public class BusFactory
    {
        private static IBus _bus;

        public static IBus Instance { get { return _bus; } }

        public static void Setup(IBus bus)
        { 
            if (_bus != null) { throw new Exception("Factory has already been configured."); }
            _bus = bus; 
        }
    }
}