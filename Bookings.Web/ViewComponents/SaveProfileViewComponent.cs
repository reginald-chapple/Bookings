using Bookings.Web.Data;
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Web.ViewComponents;

public class SaveProfileViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public SaveProfileViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke(string userId)
    {
        MatchProfile matchProfile = _context.MatchProfiles.FirstOrDefault(m => m.UserId == userId) ?? new MatchProfile
        {
            UserId = userId
        };

        ViewData["SkinComplexions"] = _context.SkinComplexions.ToList();

        // PopulateChosenAttractionData(matchProfile);

        return View(matchProfile);
    }

    // private void PopulateChosenAttractionData(MatchProfile profile)
    // {
    //     var allAttractions = _context.Attractions.ToList();
    //     var profileAttractions = new HashSet<long>(profile.Attractions.Select(t => t.AttractionId));
    //     var viewModel = new List<ChosenAttraction>();

    //     foreach (var attraction in allAttractions)
    //     {
    //         viewModel.Add(new ChosenAttraction
    //         {
    //             AttractionId = attraction.Id,
    //             Label = attraction.Label,
    //             Assigned = profileAttractions.Contains(attraction.Id)
    //         });
    //     }
    //     ViewData["Attractions"] = viewModel;
    // }
}