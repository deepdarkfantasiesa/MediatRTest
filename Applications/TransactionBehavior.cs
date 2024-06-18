using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using MediatRTest.Applications.Commands;

namespace MediatRTest.Applications
{
    public class testBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<testBehavior<TRequest, TResponse>> _logger;
        private readonly MyContext _context;
        private readonly IMediator _mediator;
        //public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger,UserContext context)
        public testBehavior(ILogger<testBehavior<TRequest, TResponse>> logger, MyContext context,IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            try
            {
                await _context.Database.BeginTransactionAsync(cancellationToken);

                //await _mediator.Publish(new CommonCommand());

                await next();

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }

    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : INotification
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly MyContext _context;
        //public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger,UserContext context)
        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, MyContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            try
            {
                var transAction = await _context.Database.BeginTransactionAsync();
                await Console.Out.WriteLineAsync($"before {transAction.TransactionId}");
                await next();

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsyncTest();
                throw;
            }
        }
    }

 

}
