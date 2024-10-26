using JobFinderAlbania.Data;
using JobFinderAlbania.Pagination;

namespace JobFinderAlbania.Filters;

public class SellerObjectQuery : UserObjectQuery
{
    public string? Education { get; set; } = string.Empty;
    
    public override async Task<PaginatedList<User?>> ApplyFilters(IQueryable<User> query, int pageIndex, int pageSize)
    {
        base.ApplyFilters(query, pageIndex, pageSize);
        if(!string.IsNullOrEmpty(Education))
            query = query.OfType<Seller>().Where(x => x.Education.Contains(Education));
        
        return await PaginatedList<User?>.CreateAsync(query, pageIndex, pageSize);
    }
}