using Bookings.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.Administrators.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrators")]
    [Route("[area]/[controller]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("/[area]/[controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}