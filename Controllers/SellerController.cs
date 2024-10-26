using JobFinderAlbania.Data;
using JobFinderAlbania.Filters;
using JobFinderAlbania.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class SellerController : Controller
{
    private readonly ISellerRepository _sellerRepository;
    private readonly UserManager<User> _userManager;
    
    
    public SellerController(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }
    
    // this method will return a list of all sellers
    public async Task<IActionResult> Index(int pageIndex, int pageSize)
    {
        var sellers = await _sellerRepository.GetPaginatedSellers(pageIndex, pageSize);
        return View(sellers);
    }
    
   
    
    // this method will return a list of all sellers filtered
    public async Task<IActionResult> GetSellerByFilters(SellerObjectQuery filters, int pageIndex, int pageSize)
    {
        var sellers = await _sellerRepository.GetSellerByFilters(filters, pageIndex, pageSize);
        return View("Index", sellers);
    }
    
}