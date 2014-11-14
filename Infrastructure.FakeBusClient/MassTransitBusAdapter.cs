using Core.CQRS;
using MassTransit;
using MassTransit.Reactive;
using System;
using System.Collections.Generic;

namespace Infrastructure.MassTransitBusAdapter
{
    public class MassTransitBusAdapter : IBus
    {
        private const int MAX_THREADS = 10;
        private const int MIN_THREADS = 2;

        private readonly IServiceBus _eventBus;
        private readonly IServiceBus _commandBus;
        private readonly IServiceBus _controlBus;

        private List<UnsubscribeAction> _unsubscribes = new List<UnsubscribeAction>();

        public MassTransitBusAdapter(string serverName, string domainName, uint numberOfThreads)
        {
            if (serverName == null ) throw new ArgumentNullException("Server name cannot be null.");
            if (domainName == null ) throw new ArgumentNullException("Domain name cannot be null.");
            if (numberOfThreads > MAX_THREADS) throw new ArgumentException("Exceeded max threads.");
            if (numberOfThreads < MIN_THREADS ) throw new ArgumentException("Must have at least 2 threads.");

            _eventBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();

                var busThreads = numberOfThreads / 2;
                sbc.SetConcurrentConsumerLimit((int)busThreads);

                sbc.ReceiveFrom("rabbitmq://" + serverName + "/" + domainName + "events?prefetch=" + busThreads);
            });

            _commandBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();

                var busThreads = numberOfThreads - (numberOfThreads / 2);
                sbc.SetConcurrentConsumerLimit((int)busThreads);

                sbc.ReceiveFrom("rabbitmq://" + serverName + "/" + domainName + "commands?prefetch=" + busThreads);
            });

            _controlBus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();
                sbc.SetConcurrentConsumerLimit(1);

                sbc.ReceiveFrom("rabbitmq://" + serverName + "/" + domainName + "control?prefetch=1");
            });
        }

        public void RegisterCommandHandler<T>(Action<T> handler) where T : Command
        {
            var action = _commandBus.SubscribeHandler<T>(handler);
            _unsubscribes.Add(action);
        }

        public void Send<T>(T command) where T : Command
        {
            _commandBus.Publish<T>(command);
        }

        public void RegisterEventSubscriber<T>(Action<T> subscriber) where T : Event 
        {
            var action = _eventBus.SubscribeHandler<T>(subscriber);
            _unsubscribes.Add(action);
        }

        public IObserver<T> AsObserver<T>() where T : Event 
        {
            return _eventBus.AsObserver<T>();
        }

        public IObservable<T> AsObservable<T>() where T : Event
        {
            return _eventBus.AsObservable<T>();
        }

        public void Publish<T>(T eventToPublish) where T : Event
        {
            _eventBus.Publish<T>(eventToPublish);
        }

        public void ShutDown()
        {
            foreach (var action in _unsubscribes)
                action();

            _eventBus.Dispose();
        }
    }
}