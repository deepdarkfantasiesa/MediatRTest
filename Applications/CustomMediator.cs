using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace MediatRTest.Applications
{
    public class CustomMediator: Mediator
    {
        private readonly ILogger<CustomMediator> _logger;
        private readonly MyContext _context;
        private readonly ConcurrentBag<Exception> _exceptions;
        private readonly CustomExceptions _customExceptions;

        public CustomMediator(CustomExceptions customExceptions,ConcurrentBag<Exception> exceptions,ILogger<CustomMediator> logger,IServiceProvider serviceFactory,MyContext myContext) : base(serviceFactory)
        {
            _customExceptions = customExceptions;
            _exceptions = exceptions;
            _logger = logger;
            _context = myContext;

        }

        

        protected override async Task PublishCore(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            try
            {
                //await _context.BeginTransactionAsyncTest();
                //Console.WriteLine($"Before all handlers execution {_context._currentTransaction.TransactionId}  {DateTime.Now}");
                //await base.PublishCore(handlerExecutors, notification, cancellationToken);
                //await _context.SaveChangesAsync();
                //Console.WriteLine($"After all handlers execution {_context._currentTransaction.TransactionId}  {DateTime.Now}");
                //await _context.CommitTransactionAsyncTest();


                await _context.BeginTransactionAsyncTest();
                Console.WriteLine($"Before all handlers execution {_context._currentTransaction.TransactionId}  {DateTime.Now}");

                INotification notification2 = notification;
                await Task.WhenAll(handlerExecutors.Select((NotificationHandlerExecutor handler) => handler.HandlerCallback(notification2, cancellationToken)).ToArray());

                _customExceptions.Exceptions.Clear();

                if (!_exceptions.IsEmpty)
                {
                    await _context.RollbackTransactionAsyncTest();
                }
                else
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"After all handlers execution {_context._currentTransaction.TransactionId}  {DateTime.Now}\n");
                    await _context.CommitTransactionAsyncTest();
                }

               

            }
            catch (Exception ex)
            {
              
            }
        }


    }

}
