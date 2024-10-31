using JobFinderAlbania.Data;
using JobFinderAlbania.Models.Service;
using JobFinderAlbania.Repositories;
using Microsoft.AspNetCore.Authorization;
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


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(new ServiceForCreationDto());
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ServiceForCreationDto serviceForCreationDto)
    {
        if (!ModelState.IsValid)
            return View(serviceForCreationDto);
        
        var user = await _userManager.GetUserAsync(User);
        
        // will check if the user is a seller or not.
        if(user is not Seller seller)
            return BadRequest("You are not a seller");
        
        var service = new Service
        {
            Name = serviceForCreationDto.Name,
            Description = serviceForCreationDto.Description,
            Price = serviceForCreationDto.Price, 
            DeliveryTime = serviceForCreationDto.DeliveryTime,
            Revisions = serviceForCreationDto.Revisions,
            Tags = serviceForCreationDto.Tags,
            IsActive = serviceForCreationDto.IsActive,
            Rating = 0,
            
            
            CategoryId = serviceForCreationDto.CategoryId,
            UserId = seller.Id
        };
        
        await _serviceRepository.CreateService(service);
        
        return RedirectToAction("Index");
        
        
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var service = await _serviceRepository.GetServiceById(id);
        
        if (service == null)
            return NotFound();
        // get the current user to check if the user is the owner of the service
        var user = await _userManager.GetUserAsync(User);
        if(service.UserId != user.Id)
            return BadRequest("You are not the owner of this service");

        var serviceForEditDto = new ServiceForEditDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            DeliveryTime = service.DeliveryTime,
            Revisions = service.Revisions,
            Tags = service.Tags,
            IsActive = service.IsActive,
            CategoryId = service.CategoryId,
        };
        
        return View(serviceForEditDto);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(ServiceForEditDto serviceForEditDto)
    {
        if (!ModelState.IsValid)
            return View(serviceForEditDto);
        
        var service = await _serviceRepository.GetServiceById(serviceForEditDto.Id);
        
        if (service == null)
            return NotFound();
        
        
        service.Name = serviceForEditDto.Name;
        service.Description = serviceForEditDto.Description;
        service.Price = serviceForEditDto.Price;
        service.DeliveryTime = serviceForEditDto.DeliveryTime;
        service.Revisions = serviceForEditDto.Revisions;
        service.Tags = serviceForEditDto.Tags;
        service.IsActive = serviceForEditDto.IsActive;
        service.CategoryId = serviceForEditDto.CategoryId;
        
        await _serviceRepository.UpdateService(service);
        
        return RedirectToAction("Index");
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _serviceRepository.GetServiceById(id);
        
        if (service == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if(user is not Seller seller)
            return BadRequest("You are not a seller");
        
        if(service.UserId != user.Id)
            return BadRequest("You are not the owner of this service");
        
        return View(service);
    }
    
}