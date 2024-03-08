using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Authorize(Roles = "Member")]
[Route("[controller]")]
public class DatingController : Controller
{
    private readonly ApplicationDbContext _context;

    public DatingController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        List<int?> complexions,
        List<int> attractions,
        int? pageNumber)
    {
        ViewData["CurrentSort"] = sortOrder;
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFilter"] = searchString;

        var profiles = from p in _context.MatchProfiles.Include(p => p.User) select p;


        if (!string.IsNullOrEmpty(searchString))
        {
            profiles = profiles.Where(p => p.User!.FullName.Contains(searchString));
        }

        if (complexions != null && complexions.Count != 0)
        {
            profiles = profiles.Where(p => complexions.Contains(p.ComplexionId));
        }

        if (attractions != null && attractions.Count != 0)
        {
            profiles = profiles.Where(p => attractions.Contains((int)p.Sexuality));
        }

        switch (sortOrder)
        {
            case "name_desc":
                profiles = profiles.OrderByDescending(p => p.User!.FullName);
                break;
            case "Date":
                profiles = profiles.OrderBy(p => p.Created);
                break;
            case "date_desc":
                profiles = profiles.OrderByDescending(p => p.Created);
                break;
            default:
                profiles = profiles.OrderBy(p => p.User!.FullName);
                break;
        }
        int pageSize = 12;

        ViewData["SkinComplexions"] = _context.SkinComplexions.ToList();
        ViewData["Attractions"] = _context.Attractions.ToList();

        return View(await PaginatedList<MatchProfile>.CreateAsync(profiles.AsNoTracking(), pageNumber ?? 1, pageSize));
    }

    [Route("Create-Profile")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProfile([Bind("Id,HasChildren,IsSmoker,IsDrinker,HairColor,EyeColor,Complexion,BodyType,GenderIdentity,UserId,ComplexionId")] MatchProfile profile /*long[] chosenAttractions*/)
    {
        if (ModelState.IsValid)
        {
            // if (chosenAttractions != null)
            // {
            //     profile.Attractions = [];
            //     foreach (var attraction in chosenAttractions)
            //     {
            //         var attractionToAdd = new ProfileAttraction { ProfileId = profile.Id, AttractionId = attraction };
            //         profile.Attractions.Add(attractionToAdd);
            //     }
            // }
            _context.Add(profile);
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers.Referer!);
        }
        return Redirect(HttpContext.Request.Headers.Referer!);
    }
}