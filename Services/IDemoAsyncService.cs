using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asyncdemo.Services
{
    public interface IDemoAsyncService
    {
        Task<dynamic> DemoMainTask(string requestId);
    }
}
