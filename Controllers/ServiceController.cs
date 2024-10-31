using JobFinderAlbania.Data;
using JobFinderAlbania.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class ServiceController : Controller
{
    
    private readonly IServiceRepository _serviceRepository;
    private readonly UserManager<User> _userManager;
    
    public ServiceController(IServiceRepository serviceRepository, UserManager<User> userManager)
    {
        _serviceRepository = serviceRepository;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index(int pageIndex = 1 , int pageSize = 10 )
    {
        var services = await _serviceRepository.GetPaginatedServices(pageIndex, pageSize);
        return View(services);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var service = await _serviceRepository.GetServiceById(id);
        
        if (service == null)
            return NotFound();
        
        return View(service);
    }
    
}