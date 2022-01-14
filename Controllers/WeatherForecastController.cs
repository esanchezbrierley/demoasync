using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asyncdemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace asyncdemo.Controllers
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
        private readonly IDemoAsyncService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDemoAsyncService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var requestId = HttpContext.TraceIdentifier;
            Console.WriteLine($"[{requestId}] - Init request");
            var result = await _service.DemoMainTask(requestId);
            Console.WriteLine($"[{requestId}] - Async demo call completed");
            Console.WriteLine($"[{requestId}] - 'result' task status: {result.Status}");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
