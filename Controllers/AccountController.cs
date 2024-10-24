using JobFinderAlbania.Data;
using JobFinderAlbania.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinderAlbania.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        var model = new RegisterStep1ViewModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterStep1ViewModel model)
    {
        if (ModelState.IsValid)
        {
            // we will store the essential data in TempData for the next step
            TempData["FirstName"] = model.FirstName;
            TempData["LastName"] = model.LastName;
            TempData["Email"] = model.Email;
            TempData["UserType"] = model.UserType;
            
            return RedirectToAction("RegisterStep2", new { userType = model.UserType });
        }
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> RegisterStep2(string userType)
    {
        if (userType == "Buyer")
        {
            var model = new BuyerRegistrationViewModel()
            {
                FirstName = TempData["FirstName"] as string,
                LastName = TempData["LastName"] as string,
                Email = TempData["Email"] as string
            };
            
            return View("BuyerRegistration", model);
        }
        else if (userType == "Seller")
        {
            var model = new SellerRegistrationViewModel()
            {
                FirstName = TempData["FirstName"] as string,
                LastName = TempData["LastName"] as string,
                Email = TempData["Email"] as string
            };
            
            return View("SellerRegistration", model);
        }
        
        return RedirectToAction("Register");
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterStep2Buyer(BuyerRegistrationViewModel buyerModel)
    {
        if (ModelState.IsValid)
        {
            var buyer = new Buyer()
            {
                FirstName = buyerModel.FirstName,
                LastName = buyerModel.LastName,
                Email = buyerModel.Email,
                CompanyName = buyerModel.CompanyName,
                PaymentMethod = buyerModel.PaymentMethod,
                Rating = 0,
                UserName = buyerModel.Email,
                NormalizedUserName = buyerModel.Email.ToUpper(),
                EmailConfirmed = true,
                NormalizedEmail = buyerModel.Email.ToUpper(),
                LockoutEnabled = true,
                JoinDate = DateTime.Today,
                Bio = buyerModel.Bio,
                
            };
            
            buyer.Id = Guid.NewGuid().ToString();
        
            var result = await _userManager.CreateAsync(buyer, buyerModel.Password);
        
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(buyer, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    
        return View("BuyerRegistration", buyerModel);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStep2Seller(SellerRegistrationViewModel sellerModel)
    {
        if (ModelState.IsValid)
        {
            var seller = new Seller()
            {
                FirstName = sellerModel.FirstName,
                LastName = sellerModel.LastName,
                Email = sellerModel.Email,
                Bio = sellerModel.Bio,
                Education = sellerModel.Education,
                Rating = 0,
                UserName = sellerModel.Email,
                NormalizedUserName = sellerModel.Email.ToUpper(),
                EmailConfirmed = true,
                NormalizedEmail = sellerModel.Email.ToUpper(),
                LockoutEnabled = true,
                JoinDate = DateTime.Today,
            };
            
            seller.Id = Guid.NewGuid().ToString();
        
            var result = await _userManager.CreateAsync(seller, sellerModel.Password);
        
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(seller, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    
        return View("SellerRegistration", sellerModel);
    }

    public  async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    // now we will add the login methods
    
    [HttpGet]
    public IActionResult Login()
    {
        var model = new LoginViewModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        
        return View(model);
    }
    
    // here will also be implemented the forgot password and reset password methods and also the lockout method later.
    
    
    
    
}