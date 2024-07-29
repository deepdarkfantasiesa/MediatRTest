using MediatR;
using MediatRTest.Applications;
using MediatRTest.Applications.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MediatRTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        static async Task SendRequest(string url, HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                //string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        [HttpGet("RequestMyAction")]
        public async Task RequestMyAction()
        {
            List<Task> tasks = new List<Task>();
            Parallel.For(0, 900, async i =>
            {
                tasks.Add(SendRequest("https://localhost:7039/WeatherForecast/test", new HttpClient()));
            });
            var startTime = DateTime.Now;
            await Task.WhenAll(tasks);
            var endTIme = DateTime.Now;
            await Console.Out.WriteLineAsync($"{startTime}--{endTIme}");

            //HttpClient client = new HttpClient();
            //var startTime = DateTime.Now;
            //for (int i = 0; i < 100; i++)
            //{

            //    await client.GetAsync("https://localhost:7039/WeatherForecast/test");
            //}
            //var endTIme = DateTime.Now;
            //await Console.Out.WriteLineAsync($"{startTime}--{endTIme}");
        }

        [HttpGet("test")]
        public async Task Test()
        {
            await _mediator.Publish(new CommonCommand());
            //await _mediator.Publish(new TestCommand());
        }
    }
}
