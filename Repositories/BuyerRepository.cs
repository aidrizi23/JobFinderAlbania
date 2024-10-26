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
    public async Task<PaginatedList<Buyer?>> GetBuyerByFilters(BuyerObjectQuery query, int pageIndex, int pageSize)
    {
        var buyers =  _dbContext.Users.OfType<Buyer>().AsNoTracking();
        if(!string.IsNullOrWhiteSpace(query.FirstName))
            buyers = buyers.Where(b => b.FirstName.Contains(query.FirstName));
        if(!string.IsNullOrWhiteSpace(query.LastName))
            buyers = buyers.Where(b => b.LastName.Contains(query.LastName));
        if(!string.IsNullOrWhiteSpace(query.CompanyName))
            buyers = buyers.Where(b => b.CompanyName.Contains(query.CompanyName));
        
        return await PaginatedList<Buyer?>.CreateAsync(buyers, pageIndex, pageSize);
        
    }
    
}

public interface IBuyerRepository
{
    Task<IEnumerable<Buyer>> GetAllBuyers();
    Task<PaginatedList<User>> GetPaginatedBuyers(int pageIndex, int pageSize);
    Task<PaginatedList<Buyer?>> GetBuyerByFilters(BuyerObjectQuery filters, int pageIndex, int pageSize);
}