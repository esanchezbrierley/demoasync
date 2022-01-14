using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace asyncdemo.Services
{
    public class DemoAsyncService: IDemoAsyncService
    {
        public DemoAsyncService(ILogger<DemoAsyncService> logger) { }

        public async Task<dynamic> DemoMainTask(string requestId) 
        {
            var task1 = DemoTaskDelay(requestId, 1, 2100);
            var task2 = DemoTaskDelay(requestId, 2, 2000);
            var task3 = DemoTaskDelay(requestId, 3, 3000);
            var task4 = DemoTaskDelay(requestId, 4, 1500);

            return Task.WhenAll(task1, task2, task3, task4);
        }

        public async Task DemoTaskDelay(string requestId, int code, int delay) 
        {
            Console.WriteLine($"[{requestId}] - Before task #{code}");
            await Task.Delay(delay);
            Console.WriteLine($"[{requestId}] - Task #{code} complete");
        }
    }
}