using Bookings.Web.Data;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.Controllers;

public class ActionItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ActionItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Label,MilestoneId")] ActionItem item)
    {
        if (ModelState.IsValid)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }

    [Route("{id}/Complete-Task")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CompleteTask(long? id, CompleteTaskModel model)
    {
        if (id == null)
        {
            return NotFound();
        }

        var actionItem = await _context.ActionItems.FindAsync(id);

        if (actionItem == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            actionItem.IsCompleted = model.IsCompleted;
            actionItem.CompletionDate = DateTime.Now;
            _context.Update(actionItem);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }

        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}