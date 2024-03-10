using Microsoft.AspNetCore.Mvc;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using System.Security.Claims;

namespace Bookings.Web.Controllers
{
    [Route("[controller]")]
    public class ExpendituresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpendituresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Cost,CampaignId")] Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                expenditure.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                _context.Add(expenditure);
                await _context.SaveChangesAsync();
                return Redirect(HttpContext.Request.Headers.Referer!);
            }
            return Redirect(HttpContext.Request.Headers.Referer!);
        }

        private bool ExpenditureExists(long id)
        {
            return _context.Expenditures.Any(e => e.Id == id);
        }
    }
}
