using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS.Exceptions
{
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message) : base(message) { }
        public IllegalStateException() { }
    }
}
