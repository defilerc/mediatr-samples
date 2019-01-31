using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRUser
{
    public class PublicMessage : INotification
    {
        public string Message { get; set; }
    }

    public class PublishHandler1 : INotificationHandler<PublicMessage>
    {
        public Task Handle(PublicMessage notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{GetType().Name}: received {notification.Message}");
            return Unit.Task;
        }
    }

    public class PublishHandler2 : INotificationHandler<PublicMessage>
    {
        public Task Handle(PublicMessage notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{GetType().Name}: received {notification.Message}");
            return Unit.Task;
        }
    }

}
