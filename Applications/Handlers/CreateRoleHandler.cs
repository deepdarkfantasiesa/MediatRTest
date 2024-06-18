using MediatR;
using MediatRTest.Applications.Commands;
using System.Collections.Concurrent;

namespace MediatRTest.Applications.Handlers
{
    public class CreateRoleHandler : INotificationHandler<CommonCommand>
    {
        private readonly ConcurrentBag<Exception> _exceptions;
        private readonly MyContext _context;
        private readonly ILogger<CreateRoleHandler> _logger;
        public CreateRoleHandler(MyContext myContext,ConcurrentBag<Exception> exceptions, ILogger<CreateRoleHandler> logger)
        {
            _exceptions = exceptions;
            _context = myContext;
            _logger = logger;

        }

        public async Task Handle(CommonCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                await Console.Out.WriteLineAsync($"GUID Role {_context.InstanceId}");
                await Console.Out.WriteLineAsync($"role {_context._currentTransaction?.TransactionId}");
                await _context.Roles.AddAsync(new Entities.Role()
                {

                    RoleCode = "test"
                });

                //await _context.SaveChangesAsync();
                //throw new InvalidOperationException("role exceptions");
            }
            catch (Exception ex)
            {
                _exceptions.Add(ex);
                _logger.LogError(ex.Message);
            }
            
            //await Task.Delay(5000).WaitAsync(cancellationToken);

            //await _context.SaveChangesAsync();
        }
    }
}
