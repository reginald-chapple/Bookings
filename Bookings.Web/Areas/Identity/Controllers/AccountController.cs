using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Bookings.Web.Controllers;

namespace Bookings.Web.Identity.Controllers
{
    [Authorize]
    [Area("Identity")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [TempData]
        public string ErrorMessage { get; set; } = string.Empty;

        // [HttpGet]
        // [AllowAnonymous]
        // public IActionResult Register(string? returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     return View();
        // }

        // [HttpPost]
        // [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Register(UserInputModel user, string? returnUrl = null)
        // {
        //     ViewData["ReturnUrl"] = returnUrl;
        //     if (ModelState.IsValid)
        //     {
        //         string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/members");
        //         string imageName = Guid.NewGuid().ToString() + "_" + user.ImageUpload!.FileName;
        //         string filePath = Path.Combine(uploadsDir, imageName);
                
        //         FileStream fs = new FileStream(filePath, FileMode.Create);
        //         await user.ImageUpload.CopyToAsync(fs);
        //         fs.Close();

        //         ApplicationUser newUser = new ApplicationUser 
        //         { 
        //             Id = Guid.NewGuid().ToString(),
        //             FullName = user.FullName,
        //             UserName = user.Email, 
        //             Email = user.Email,
        //             Image = imageName
        //         };

        //         IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

        //         await _userManager.AddToRoleAsync(newUser, "Member");

        //         if (result.Succeeded)
        //         {
        //             await _signInManager.SignInAsync(newUser, isPersistent: false);
        //             return RedirectToLocal(returnUrl!);
        //         }

        //         foreach (IdentityError error in result.Errors)
        //         {
        //             ModelState.AddModelError("", error.Description);
        //         }

        //     }

        //     return View(user);
        // }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null) 
        {

            ViewData["ReturnUrl"] = returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await _signInManager.SignOutAsync();


            return View(new AuthenticationModel());
        } 

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticationModel loginVM, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl!);
                }

                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(loginVM);
        }

        [HttpGet]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}
