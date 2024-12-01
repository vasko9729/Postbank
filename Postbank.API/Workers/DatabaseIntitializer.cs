
using Postbank.Application.Seeder;
using Postbank.Infrastructure.Seeder;

namespace Postbank.API.Workers;

public class DatabaseIntitializer : IHostedLifecycleService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseIntitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        using (var sp = _serviceProvider.CreateScope())
        {
            var seeder = sp.ServiceProvider.GetRequiredService<ISeeder>();
            await seeder.SeedAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
