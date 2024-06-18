using MediatR;
using MediatRTest.Applications.Commands;

namespace MediatRTest.Applications.Handlers
{
    public class MyRequestHandler : IRequestHandler<MyRequestCommand,bool>
    {
        private readonly IMediator _mediator;
        public MyRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(MyRequestCommand request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("this is request");
            await _mediator.Publish(new CommonCommand());
            return true;
        }
    }
}
