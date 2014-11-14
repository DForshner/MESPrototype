using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface ISendCommands
    {
        void Send<T>(T command) where T : Command;
    }

    public static class ISendCommandsExtensions
    {
        public static void SendAll<T>(this ISendCommands bus, IEnumerable<T> commands) where T : Command
        {
            foreach(var cmd in commands)
                bus.Send<T>(cmd);
        }
    }
}
