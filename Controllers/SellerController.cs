using JobFinderAlbania.Data;
using JobFinderAlbania.Filters;
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
    
}