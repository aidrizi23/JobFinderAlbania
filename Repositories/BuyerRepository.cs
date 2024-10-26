using JobFinderAlbania.Data;
using JobFinderAlbania.Filters;
using JobFinderAlbania.Pagination;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.Repositories;

public class BuyerRepository : IBuyerRepository
{
    
    private readonly ApplicationDbContext _dbContext;
    
    public BuyerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // this method gets all buyers
    public async Task<IEnumerable<Buyer>> GetAllBuyers()
    {
        return await _dbContext.Buyers.AsNoTracking().ToListAsync();
    }
    
    // this method gets all buyers paginated
    public async Task<PaginatedList<User>> GetPaginatedBuyers(int pageIndex, int pageSize)
    {
        return await PaginatedList<User>.CreateAsync(_dbContext.Users.AsNoTracking().OfType<Buyer>(), pageIndex, pageSize);
    }
    
    // this method gets filtered buyers
    public async Task<PaginatedList<User?>> GetBuyerByFilters(BuyerObjectQuery filters, int pageIndex, int pageSize)
    {
        var buyers = _dbContext.Buyers.AsNoTracking();
        return await filters.ApplyFilters(buyers, pageIndex, pageSize);
    }
    
}

public interface IBuyerRepository
{
    Task<IEnumerable<Buyer>> GetAllBuyers();
    Task<PaginatedList<User>> GetPaginatedBuyers(int pageIndex, int pageSize);
    Task<PaginatedList<User?>> GetBuyerByFilters(BuyerObjectQuery filters, int pageIndex, int pageSize);
}