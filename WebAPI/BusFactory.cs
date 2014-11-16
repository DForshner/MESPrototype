using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI 
{
    /// <summary>
    /// TODO: Change to IoC
    /// </summary>
    public class BusFactory
    {
        private static IBus _bus;

        public static IBus Instance { get { return _bus; } }
    }
}