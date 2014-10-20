using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface IBus : ISendCommands, IPublishEvents
    {
        void RegisterCommandHandler<C>(Action<C> handler) where C : Command;
        void RegisterEventSubscriber<E>(Action<E> subscriber) where E : Event;
    }
}