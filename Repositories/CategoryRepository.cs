using JobFinderAlbania.Data;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        
        return await _context.Categories.FindAsync(id);
        
    }
    
    public async Task<Category?> GetCategoryByName(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }
    

}


public interface ICategoryRepository
{
    IEnumerable<Category> GetAllCategories();
    Task<Category?> GetCategoryById(int id);
    Task<Category?> GetCategoryByName(string name);
}