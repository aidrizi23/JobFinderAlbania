using JobFinderAlbania.Data;
using JobFinderAlbania.Filters;
using JobFinderAlbania.Pagination;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public SellerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // this method gets all sellers
    public async Task<IEnumerable<Seller>> GetAllSellers()
    {
        return await _dbContext.Sellers.AsNoTracking().ToListAsync();
    }
    
    // this method gets all sellers paginated
    public async Task<PaginatedList<User>> GetPaginatedSellers(int pageIndex, int pageSize)
    {
        return await PaginatedList<User>.CreateAsync(_dbContext.Users.AsNoTracking().OfType<Seller>(), pageIndex, pageSize);
    }
    
    // this method gets filtered sellers
    public async Task<PaginatedList<User?>> GetSellerByFilters(SellerObjectQuery filters, int pageIndex, int pageSize)
    {
        var sellers =  _dbContext.Sellers.AsNoTracking();
        return await filters.ApplyFilters(sellers, pageIndex, pageSize);
    }
    
}

public interface ISellerRepository
{
    Task<IEnumerable<Seller>> GetAllSellers();
    Task<PaginatedList<User>> GetPaginatedSellers(int pageIndex, int pageSize);
    Task<PaginatedList<User?>> GetSellerByFilters(SellerObjectQuery filters, int pageIndex, int pageSize);
}