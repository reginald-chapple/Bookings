using System.Security.Claims;
using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class ResumesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ResumesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Save")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([Bind("Id,Skills,Experience,Education,Background")] Resume resume)
    {
        if (ModelState.IsValid)
        {
            resume.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            resume.Applicant = User.FindFirst("FullName")!.Value;
            await _context.AddAsync(resume);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}