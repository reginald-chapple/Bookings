using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveResumeViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public SaveResumeViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke(string userId)
    {
        Resume resume = _context.Resumes.FirstOrDefault(m => m.CreatedBy == userId) ?? new Resume
        {
            CreatedBy = userId
        };

        return View(resume);
    }
}