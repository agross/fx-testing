using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;

namespace MT.Backend
{
  public class Worker : IHostedService
  {
    readonly IBusControl _bus;

    public Worker(IBusControl bus)
    {
      _bus = bus;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken) => _bus.StopAsync(cancellationToken);
  }
}
