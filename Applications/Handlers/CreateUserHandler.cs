using MediatR;
using MediatRTest.Applications.Commands;
using System.Collections.Concurrent;

namespace MediatRTest.Applications.Handlers
{
    public class CreateUserHandler : INotificationHandler<CommonCommand>
    {
        private readonly MyContext _context;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly ConcurrentBag<Exception> _exceptions;
        public CreateUserHandler(MyContext myContext,ConcurrentBag<Exception> exceptions, ILogger<CreateUserHandler> logger)
        {
            _context = myContext;
            _exceptions = exceptions;
            _logger = logger;
        }
        public async Task Handle(CommonCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                await Console.Out.WriteLineAsync($"GUID User {_context.InstanceId}");
                await Console.Out.WriteLineAsync($"user {_context._currentTransaction?.TransactionId}");
                await _context.Users.AddAsync(new Entities.User()
                {
                    Name = "114"
                });


                //await Task.Delay(15000).WaitAsync(cancellationToken);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                _logger.LogError(ex.Message);
            }
        }
    }
}
