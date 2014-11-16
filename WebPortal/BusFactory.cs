using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal
{
    /// <summary>
    /// TODO: Change to IoC
    /// </summary>
    public static class BusFactory
    {
        private static IBus _bus;

        public static IBus Instance { get { return _bus; } }
    }
}