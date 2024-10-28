using JobFinderAlbania.Data;
using JobFinderAlbania.Filters;
using JobFinderAlbania.Models.Seller;
using JobFinderAlbania.Pagination;
using JobFinderAlbania.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class SellerController : Controller
{
    private readonly ISellerRepository _sellerRepository;
    private readonly UserManager<User> _userManager;
    
    public SellerController(UserManager<User> userManager, ISellerRepository sellerRepository)
    {
        _userManager = userManager;
        _sellerRepository = sellerRepository;
    }
    
    // this method will return a list of all sellers
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index(int pageIndex = 1 , int pageSize = 10)
    {
        var sellers = await _sellerRepository.GetPaginatedSellers(pageIndex, pageSize);
        return View(sellers);
    }
    
   
    
    // this method will return a list of all sellers filtered
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSellerByFilters(SellerObjectQuery filters, int pageIndex, int pageSize)
    {
        var sellers = await _sellerRepository.GetSellerByFilters(filters, pageIndex, pageSize);
        ViewData["filters"] = filters;
        return View("Index", sellers);
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(string id)
    {
        var seller = await _sellerRepository.GetSellerById(id);
        
        if (seller == null)
            return NotFound();
        
        // will be using mapping and a dto for this just so that we can choose the data to display in the backend easier
        var dto = new SellerDetailsViewModel()
        {
            FirstName = seller.FirstName,
            LastName = seller.LastName,
            Email = seller.Email!,
            PhoneNumber = seller.PhoneNumber ?? "No phone number",
            Bio = seller.Bio,
            ProfilePicture = seller.ProfilePicture ?? "/images/default-profile.jpg", // make sure to add a default profile picture here in the future
            Education = seller.Education,
            JoinDate = seller.JoinDate
        };
        
        return View(dto);
    }
}