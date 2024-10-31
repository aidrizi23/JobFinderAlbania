using JobFinderAlbania.Filters;
using JobFinderAlbania.Models.Buyer;
using JobFinderAlbania.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class BuyerController : Controller
{
    private readonly IBuyerRepository _buyerRepository;
    
    public BuyerController(IBuyerRepository buyerRepository)
    {
        _buyerRepository = buyerRepository;
    }
    
    // this method will return a list of all buyers
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index(int pageIndex = 1 , int pageSize = 10)
    {
        var buyers = await _buyerRepository.GetPaginatedBuyers(pageIndex, pageSize);
        return View(buyers);
    }
    
    // this method will return a list of all buyers filtered
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetBuyerByFilters(BuyerObjectQuery filters, int pageIndex, int pageSize)
    {
        var buyers = await _buyerRepository.GetBuyerByFilters(filters, pageIndex, pageSize);
        ViewData["filters"] = filters;
        return View("Index", buyers);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(string id)
    {
        var buyer = await _buyerRepository.GetBuyerById(id);
        
        if (buyer == null)
            return NotFound();
        
        // will be using mapping and a dto for this just so that we can choose the data to display in the backend easier
        var dto = new BuyerDetailsViewModel
        {
            FirstName = buyer.FirstName,
            LastName = buyer.LastName,
            Email = buyer.Email!,
            PhoneNumber = buyer.PhoneNumber ?? "No phone number",
            Bio = buyer.Bio ?? "No bio",
            ProfilePicture = buyer.ProfilePicture ?? "  /images/default-profile.jpg",
            JoinDate = buyer.JoinDate
        };
        
        return View(dto);
    }
}