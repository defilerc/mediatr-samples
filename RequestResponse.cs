using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRUser
{
    public class PrivateMessage : IRequest<string>
    {
        public string Message { get; set; }
    }

    public class MessageHandler : IRequestHandler<PrivateMessage, string>
    {
        public Task<string> Handle(PrivateMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"I received: {request.Message}");
            Console.WriteLine($"and i'm responding with {request.Message}-ACK");

            return Task.FromResult($"{request.Message}-ACK");
        }
    }
}
