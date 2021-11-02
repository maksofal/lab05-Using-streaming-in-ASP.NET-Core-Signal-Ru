using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Connections;

namespace lab04
{
    public class ChatHub : Hub 
    {
        private Random random = new Random();
        private Random time = new Random();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            this._logger = logger;
        }

        public async IAsyncEnumerable<int> Send(int col)
        {
            int c = 0;
            for (int i = 0; i < col; i++)
            {
                c = random.Next(0, 50);
                await Task.Delay(time.Next(1000, 5000));
                yield return c;
                this._logger.LogInformation($"\r\n{c}\t");
            }
        }



        public async Task WriteItems(ChannelWriter<int> write, int count)
        {
            for (int i = 0; i< count; i++)
            {
                await write.WriteAsync(random.Next(0, 50));
                await Task.Delay(time.Next(1000, 5000));
            }
        }
    }
}
