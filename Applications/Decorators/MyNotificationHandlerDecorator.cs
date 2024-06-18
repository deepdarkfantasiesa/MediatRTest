using MediatR;

namespace MediatRTest.Applications.Decorators
{
    //public class MyNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
    //where TNotification : INotification
    //{
    //    private readonly INotificationHandler<TNotification> _inner;
    //    private readonly MyContext _context;

    //    public MyNotificationHandlerDecorator(INotificationHandler<TNotification> inner,MyContext myContext)
    //    {
    //        _inner = inner;
    //        _context = myContext;
    //    }

    //    public async Task Handle(TNotification notification, CancellationToken cancellationToken)
    //    {
    //        // 在这里添加你的逻辑
    //        // ...
    //        try
    //        {
    //            var id = await _context.BeginTransactionAsyncTest();
    //            //await Console.Out.WriteLineAsync($"{id.TransactionId}");
    //            //await Console.Out.WriteLineAsync($"{_context.Database.CurrentTransaction.TransactionId}");
    //            await _inner.Handle(notification, cancellationToken);
    //            //await Console.Out.WriteLineAsync($"{_context.Database.CurrentTransaction.TransactionId}");
    //            await _context.SaveChangesAsync(cancellationToken);

    //            await _context.Database.CommitTransactionAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            //await Console.Out.WriteLineAsync($"{_context.Database.CurrentTransaction.TransactionId}");
    //            await _context.Database.RollbackTransactionAsync(cancellationToken);

    //        }
    //    }
    //}

}
