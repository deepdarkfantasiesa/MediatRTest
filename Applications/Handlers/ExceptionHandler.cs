using MediatR;
using MediatRTest.Applications.Commands;
using System.Collections.Concurrent;

namespace MediatRTest.Applications.Handlers
{
    public class ExceptionHandler : INotificationHandler<CommonCommand>
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly MyContext _context;
        private readonly ConcurrentBag<Exception> _exceptions;
        public ExceptionHandler(ILogger<ExceptionHandler> logger, MyContext context,ConcurrentBag<Exception> exceptions)
        {
            _exceptions = exceptions;
            _logger = logger;
            _context = context;
        }
        public async Task Handle(CommonCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                await Console.Out.WriteLineAsync($"GUID Exce {_context.InstanceId}");
                await Console.Out.WriteLineAsync($"exce {_context._currentTransaction?.TransactionId}");
                //await Task.Delay(10000).WaitAsync(cancellationToken);

                //throw new AggregateException("exception");
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                _logger.LogError(ex.Message);
            }
        

        }
    }
}
