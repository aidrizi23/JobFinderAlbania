using JobFinderAlbania.Data;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.BackgroundServices;

// this code is mostly produced by chat gpt as I was not familiar enough with the background services 😁
public class AccountDeletionService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;

    public AccountDeletionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Set the timer to call DoWork every hour (adjust as needed)
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        // Create a scope to resolve scoped services
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await DeleteAccountsAsync(dbContext);
        }
    }

    private async Task DeleteAccountsAsync(ApplicationDbContext dbContext)
    {
        var usersToDelete = await dbContext.Users
            .Where(u => u.LockoutEnd < DateTimeOffset.Now && u.AccountDeletionRequested)
            .ToListAsync();

        foreach (var user in usersToDelete)
        {
            dbContext.Users.Remove(user);
        }
        await dbContext.SaveChangesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}