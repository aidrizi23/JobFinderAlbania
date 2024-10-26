using JobFinderAlbania.Data;
using JobFinderAlbania.Pagination;

namespace JobFinderAlbania.Filters;

public class BuyerObjectQuery : UserObjectQuery
{
    public string? CompanyName { get; set; } = string.Empty;

    public override async Task<PaginatedList<User?>> ApplyFilters(IQueryable<User> query, int pageIndex, int pageSize)
    {
        base.ApplyFilters(query, pageIndex, pageSize);
        if(!string.IsNullOrEmpty(CompanyName))
            query = query.OfType<Buyer>().Where(x => x.CompanyName.Contains(CompanyName));
        
        return await PaginatedList<User?>.CreateAsync(query, pageIndex, pageSize);
    }

    
}