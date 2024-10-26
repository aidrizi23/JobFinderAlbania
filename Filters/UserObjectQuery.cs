using JobFinderAlbania.Data;
using JobFinderAlbania.Pagination;

namespace JobFinderAlbania.Filters;

public class UserObjectQuery
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;

    public virtual async Task<PaginatedList<User?>> ApplyFilters(IQueryable<User> query, int pageIndex, int pageSize)
    {
        if(!string.IsNullOrEmpty(FirstName))
            query = query.Where(x => x.FirstName.Contains(FirstName));  
        if(!string.IsNullOrEmpty(LastName))
            query = query.Where(x => x.LastName.Contains(LastName));
        
        return await PaginatedList<User?>.CreateAsync(query, pageIndex, pageSize);
    }

}