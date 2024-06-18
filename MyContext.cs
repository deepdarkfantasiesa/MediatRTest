using MediatRTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MediatRTest
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public IDbContextTransaction _currentTransaction;
        public Guid InstanceId { get; } = Guid.NewGuid();

        public async Task<IDbContextTransaction> BeginTransactionAsyncTest()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsyncTest()
        {
            _currentTransaction = null;
            await Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsyncTest()
        {
            _currentTransaction = null;
            await Database.RollbackTransactionAsync();
        }
    }
}
