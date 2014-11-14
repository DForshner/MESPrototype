using System;
using System.Collections.Generic;

namespace Core.CQRS
{
    public interface IBus : ISendCommands, IPublishEvents
    {
        void RegisterCommandHandler<C>(Action<C> handler) where C : Command;

        void RegisterEventSubscriber<E>(Action<E> subscriber) where E : Event;

        IObserver<T> AsObserver<T>() where T : Event;

        IObservable<T> AsObservable<T>() where T : Event;
    }

    public static class IBusExtensions
    {
        public static IEnumerable<T> SendTo<T>(this IEnumerable<T> commands, IBus bus) where T : Command
        {
            foreach (var cmd in commands)
            {
                bus.Send<T>(cmd);
            }

            return commands;
        }
    }
}