using MediatR;
using MediatRTest.Applications.Commands;

namespace MediatRTest.Applications.Handlers
{
	public class TestHandler : INotificationHandler<TestCommand>,INotificationHandler<CommonCommand>
	{
        public TestHandler()
        {
            
        }

        public async Task Handle(TestCommand notification, CancellationToken cancellationToken)
		{
            await Console.Out.WriteLineAsync("this is TestHandler");
        }

		public async Task Handle(CommonCommand notification, CancellationToken cancellationToken)
		{
            await Console.Out.WriteLineAsync("this is CommandHandler");
        }
	}
}
