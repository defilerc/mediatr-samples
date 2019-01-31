using Autofac;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRUser
{
    class Program
    {
        private static IContainer Container { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Sending msg through the mediator!");

            var builder = new ContainerBuilder();

            builder.RegisterType<CustomMediator>().As<IMediator>().InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<MessageHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<PublishHandler1>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<PublishHandler2>().AsImplementedInterfaces().InstancePerDependency();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                var message = new PrivateMessage { Message = "Kostas" };
                var response = await mediator.Send(message);

                var publishMessage = new PublicMessage { Message = "Publish-Message" };
                await mediator.Publish(publishMessage);
            }
        }
    }


    public class CustomMediator : Mediator
    {
        public CustomMediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            throw new NotImplementedException();
        }

        public new Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.Send(request, cancellationToken);
        }
    }
}
