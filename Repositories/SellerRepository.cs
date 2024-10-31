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
        return await _dbContext.Sellers
            .Include(x => x.Services)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }
    
    // this method gets all sellers paginated
    public async Task<PaginatedList<Seller>> GetPaginatedSellers(int pageIndex, int pageSize)
    {
        return await PaginatedList<Seller>.CreateAsync(_dbContext.Users
            .AsNoTracking()
            .OfType<Seller>()
            .Include(x => x.Services), pageIndex, pageSize);
    }
    
    // this method gets the seller by Id    
    public async Task<Seller?> GetSellerById(string id)
    {
        return await  _dbContext.Sellers
            .Include(x => x.Services)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    // this method gets filtered sellers
    public async Task<PaginatedList<Seller>> GetSellerByFilters(SellerObjectQuery query, int pageIndex, int pageSize)
    {
       
        var sellers = _dbContext.Users.OfType<Seller>().AsNoTracking();


        if (!string.IsNullOrWhiteSpace(query.FirstName))
            sellers = sellers.Where(s => s.FirstName.ToLower().Contains(query.FirstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(query.LastName))
            sellers = sellers.Where(s => s.LastName.ToLower().Contains(query.LastName.ToLower()));
        
        return await PaginatedList<Seller>.CreateAsync(sellers.Include(x => x.Services), pageIndex, pageSize);
    }
    
}

public interface ISellerRepository
{
    Task<IEnumerable<Seller>> GetAllSellers();
    Task<PaginatedList<Seller>> GetPaginatedSellers(int pageIndex, int pageSize);
    Task<Seller?> GetSellerById(string id);
    Task<PaginatedList<Seller?>> GetSellerByFilters(SellerObjectQuery filters, int pageIndex, int pageSize);
}