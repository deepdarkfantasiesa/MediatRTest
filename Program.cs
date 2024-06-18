
using MediatR;
using MediatR.NotificationPublishers;
using MediatRTest.Applications;
using MediatRTest.Applications.Decorators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Concurrent;

namespace MediatRTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MyCustomPipelineBehavior<,>));


            //builder.Services.AddScoped(typeof(INotificationHandler<>), typeof(MyNotificationHandlerDecorator<>));
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
                //cfg.NotificationPublisher = new TaskWhenAllPublisher();
                //cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
                //cfg.AddOpenBehavior(typeof(testBehavior<,>));
                //cfg.AddOpenBehavior(typeof(MyCustomPipelineBehavior<,>));
            });

            builder.Services.AddScoped<IMediator, CustomMediator>();


            builder.Services.AddDbContextPool<MyContext>(options =>
            {
                options.UseMySql(builder.Configuration["ConnectionString"], ServerVersion.AutoDetect(builder.Configuration["ConnectionString"]));
            });

            //builder.Services.AddDbContext<MyContext>(options =>
            //    options.UseMySql(builder.Configuration["ConnectionString"], ServerVersion.AutoDetect(builder.Configuration["ConnectionString"])),
            //    ServiceLifetime.Singleton);

            builder.Services.TryAddScoped(_ => new ConcurrentBag<Exception>());
            builder.Services.TryAddScoped(_ => new CustomExceptions());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
