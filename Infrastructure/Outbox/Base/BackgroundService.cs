using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ChatApp.Infrastructure.Outbox
{
    public abstract class BackgroundService: Microsoft.Extensions.Hosting.BackgroundService
    {
        //protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
    }
}