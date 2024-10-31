using System.Collections;
using JobFinderAlbania.Data;
using JobFinderAlbania.Pagination;
using Microsoft.EntityFrameworkCore;

namespace JobFinderAlbania.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;
    
    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Service?> GetServiceById(int id)
    {
        return await _context.Services
            .Include(s => s.Category)
            .Include(s => s.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<IEnumerable<Service>> GetAllServices()
    {
        return await _context.Services
            .Include(s => s.Category)
            .Include(s => s.User)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<PaginatedList<Service>> GetPaginatedServices( int pageIndex, int pageSize)
    {
        var query = _context.Services
            .Include(s => s.Category)
            .Include(s => s.User)
            .AsSplitQuery()
            .AsNoTracking();
        
        return await PaginatedList<Service>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<IEnumerable<Service?>> GetServiceByUserId(string userId)
    {
        
        var services = await _context.Services.Where(x => x.UserId == userId)
            .Include(x => x.Category)
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();
        return services;
    }
    
    
    // CRUD operations
    public async Task CreateService(Service service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateService(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteService(Service service)
    {
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ServiceExists(int id)
    {
        return await _context.Services.AnyAsync(s => s.Id == id);
    }
}



public interface IServiceRepository
{
    Task<Service?> GetServiceById(int id);
    Task<IEnumerable<Service>> GetAllServices();
    Task<PaginatedList<Service>> GetPaginatedServices(int pageIndex, int pageSize);
    Task<IEnumerable<Service?>> GetServiceByUserId(string userId);
    
    Task CreateService(Service service);
    Task UpdateService(Service service);
    Task DeleteService(Service service);
    Task<bool> ServiceExists(int id);
}