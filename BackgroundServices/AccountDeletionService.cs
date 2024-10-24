using JobFinderAlbania.Data;

namespace JobFinderAlbania.BackgroundServices;

// this code is mostly produced by chat gpt as I was not familiar enough with the background services 😁
public class AccountDeletionService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ApplicationDbContext _dbContext;
    private Timer _timer;

    public AccountDeletionService(IServiceScopeFactory scopeFactory, ApplicationDbContext dbContext)
    {
        _scopeFactory = scopeFactory;
        _dbContext = dbContext;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Run the task every day (e.g., check at midnight daily)
        _timer = new Timer(DeleteExpiredAccounts, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private async void DeleteExpiredAccounts(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {

            // Query users where AccountDeletionRequested is true and LockoutEnd has passed
            var usersToDelete = _dbContext.Users
                .Where(u => u.AccountDeletionRequested && u.LockoutEnd <= DateTimeOffset.UtcNow)
                .ToList();

            if (usersToDelete.Any())
            {
                _dbContext.Users.RemoveRange(usersToDelete); // Remove users from the database (in mass)
                await _dbContext.SaveChangesAsync(); 
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0); // Stop the timer when service stops
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}