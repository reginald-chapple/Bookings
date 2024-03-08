
using Bookings.Web.Domain;
using Microsoft.AspNetCore.Identity;

namespace Bookings.Web.Data.Services;

public class SeederService : ISeederService
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeederService(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAttractionsAsync()
    {
        if (!_context.Attractions.Any())
        {
            await _context.Attractions.AddRangeAsync([
                new Attraction{ Label = "Men" },
                new Attraction{ Label = "Women" },
                new Attraction{ Label = "Lesbian" },
                new Attraction{ Label = "Gay" },
                new Attraction{ Label = "Bisexual Women" },
                new Attraction{ Label = "Bisexual Men" },
                new Attraction{ Label = "Transgender Men" },
                new Attraction{ Label = "Transgender Women" },
                new Attraction{ Label = "Non-binary" },
            ]);
        }
    }

    public async Task SeedCausesAsync()
    {
        if (!_context.Causes.Any())
        {
            await _context.Causes.AddRangeAsync([
                new Cause { Id = 1, Name = "Agriculture and fishing", Slug = "agriculture-and-fishing" },
                new Cause { Id = 2, Name = "Community development", Slug = "community-development" },
                new Cause { Id = 3, Name = "Environment", Slug = "environment" },
                new Cause { Id = 4, Name = "Shelter and residential care", Slug = "shelter-and-residential-care" },
                new Cause { Id = 5, Name = "Human rights" , Slug = "human-rights" },
                new Cause { Id = 6, Name = "Human services", Slug = "human-services" },
                new Cause { Id = 7, Name = "Environmental justice", Slug = "environmental-justice", ParentId = 3 },
                new Cause { Id = 8, Name = "Climate change", Slug = "climate-change", ParentId = 3 },
                new Cause { Id = 9, Name = "Natural resources", Slug = "natural-resources", ParentId = 3 },
                new Cause { Id = 10, Name = "Hazardous waste management", Slug = "hazardous-waste-management", ParentId = 3 },
                new Cause { Id = 11, Name = "Water resources" , Slug = "water-resources", ParentId = 3 },
                new Cause { Id = 12, Name = "Land resources", Slug = "land-resources", ParentId = 3 },
                new Cause { Id = 13, Name = "Energy resources", Slug = "energy-resources", ParentId = 3 },
                new Cause { Id = 14, Name = "Biodiversity", Slug = "biodiversity", ParentId = 3 },
                new Cause { Id = 15, Name = "Domesticated animals", Slug = "domesticated-animals", ParentId = 3 },
                new Cause { Id = 16, Name = "Environmental education", Slug = "hazardous-waste-management", ParentId = 3 },
                new Cause { Id = 17, Name = "Agriculture" , Slug = "agriculture", ParentId = 1 },
                new Cause { Id = 18, Name = "Food security", Slug = "food-security", ParentId = 1 },
                new Cause { Id = 19, Name = "Fishing and aquaculture" , Slug = "fishing-and-aquaculture", ParentId = 1 },
                new Cause { Id = 20, Name = "Sustainable development", Slug = "sustainable-development", ParentId = 2 },
                new Cause { Id = 21, Name = "Community improvement" , Slug = "community-improvement", ParentId = 2 },
                new Cause { Id = 22, Name = "Community organizing", Slug = "community-organizing", ParentId = 2 },
                new Cause { Id = 23, Name = "Neighborhood associations" , Slug = "neighborhood-associations", ParentId = 2 },
                new Cause { Id = 24, Name = "Foreclosure prevention", Slug = "foreclosure-prevention", ParentId = 4 },
                new Cause { Id = 25, Name = "Housing rehabilitation" , Slug = "community-improvement", ParentId = 4 },
                new Cause { Id = 26, Name = "Public housing", Slug = "public-housing", ParentId = 4 },
                new Cause { Id = 27, Name = "Home ownership" , Slug = "home-ownership", ParentId = 4 },
                new Cause { Id = 28, Name = "Housing loss prevention", Slug = "housing-loss-prevention", ParentId = 4 },  
                new Cause { Id = 29, Name = "Tenants' organizations", Slug = "tenants-organizations", ParentId = 4 }, 
                new Cause { Id = 30, Name = "Supportive housing", Slug = "supportive-housing", ParentId = 4 },
                new Cause { Id = 31, Name = "Housing services" , Slug = "housing-services", ParentId = 4 },
                new Cause { Id = 32, Name = "Homeless shelters", Slug = "homeless-shelters", ParentId = 4 },
                new Cause { Id = 34, Name = "Temporary accommodations", Slug = "temporary-accommodations", ParentId = 4 },   
                new Cause { Id = 35, Name = "Individual liberties", Slug = "individual-liberties", ParentId = 5 },
                new Cause { Id = 36, Name = "Social rights" , Slug = "social-rights", ParentId = 5 },
                new Cause { Id = 37, Name = "Justice rights", Slug = "justice-rights", ParentId = 5 },
                new Cause { Id = 38, Name = "Antidiscrimination" , Slug = "antidiscrimination", ParentId = 5 },
                new Cause { Id = 39, Name = "Diversity and intergroup relations", Slug = "Diversity and intergroup relations", ParentId = 5 },
                new Cause { Id = 40, Name = "Human services information", Slug = "human-services-information", ParentId = 6 },    
                new Cause { Id = 41, Name = "Basic and emergency aid", Slug = "basic-and-emergency-aid", ParentId = 6 },    
            ]);
        }
    }

    public async Task SeedUsersAsync()
    {
        if (!_context.Roles.Any())
        {
            await CreateRoleAsync("Administrator");
            await CreateRoleAsync("Member");
        }

        if (!_context.Users.Any())
        {
            await CreateUserAsync("Super User", "sudo@local.com", "sudo@local.com", "P@ss1234", "blank.jpg", "Administrator");
            await CreateUserAsync("Aaliyah Ayad", "aaliyah.ayad@local.com", "aaliyah.ayad@local.com", "P@ss1234", "aaliyah.jpg", "Member");
            await CreateUserAsync("Andrew Chan", "andrew.chan@local.com", "andrew.chan@local.com", "P@ss1234", "andrew.jpg", "Member");
            await CreateUserAsync("Ayden Ross", "ayden.ross@local.com", "ayden.ross@local.com", "P@ss1234", "ayden.jpg", "Member");
            await CreateUserAsync("Cali Greene", "cali.greene@local.com", "cali.greene@local.com", "P@ss1234", "cali.jpg", "Member");
            await CreateUserAsync("Eddie Moss", "eddie.moss@local.com", "eddie.moss@local.com", "P@ss1234", "eddie.jpg", "Member");
            await CreateUserAsync("Evie Diaz", "evie.diaz@local.com", "evie.diaz@local.com", "P@ss1234", "evie.jpg", "Member");
            await CreateUserAsync("Khalil Long", "khalil.long@local.com", "khalil.long@local.com", "P@ss1234", "khalil.jpg", "Member");
            await CreateUserAsync("Lany-Jade Mondou", "lanyjade.mondou@local.com", "lanyjade.mondou.long@local.com", "P@ss1234", "lanyjade.jpg", "Member");
            await CreateUserAsync("Layla Denlea", "layla.denlea@local.com", "layla.denlea@local.com", "P@ss1234", "layla.jpg", "Member");
            await CreateUserAsync("Lulu Wright", "lulu.wright@local.com", "lulu.wright@local.com", "P@ss1234", "lulu.jpg", "Member");
            await CreateUserAsync("Margot Hart", "margot.hart@local.com", "margot.hart@local.com", "P@ss1234", "margot.jpg", "Member");
            await CreateUserAsync("Mary Mack", "mary.mack@local.com", "mary.mack@local.com", "P@ss1234", "mary.jpg", "Member");
            await CreateUserAsync("Maya Nguyen", "maya.nguyen@local.com", "maya.nguyen@local.com", "P@ss1234", "maya.jpg", "Member");
            await CreateUserAsync("Mehmet Mammadii", "mehmet.mammadii@local.com", "mehmet.mammadii@local.com", "P@ss1234", "mehmet.jpg", "Member");
            await CreateUserAsync("Min An", "min.an@local.com", "min.an@local.com", "P@ss1234", "min-an.jpg", "Member");
            await CreateUserAsync("Noah Jones", "noah.jones@local.com", "noah.jones@local.com", "P@ss1234", "noah.jpg", "Member");
            await CreateUserAsync("Rosa Santoso", "rosa.santoso@local.com", "rosa.santoso@local.com", "P@ss1234", "rosa.jpg", "Member");
            await CreateUserAsync("Sammy Bear", "sammy.bear@local.com", "sammy.bear@local.com", "P@ss1234", "sammy.jpg", "Member");
            await CreateUserAsync("Wade Rivers", "wade.rivers@local.com", "wade.rivers@local.com", "P@ss1234", "wade.jpg", "Member");
            await CreateUserAsync("Zoey Rice", "zoey.rice@local.com", "zoey.rice@local.com", "P@ss1234", "zoey.jpg", "Member");
        }
    }

    public async Task CreateUserAsync(string fullName, string userName, string email, string password, string imageName, string role)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            FullName = fullName,
            UserName = userName,
            Email = email,
            Image = imageName
        };

        await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task CreateRoleAsync(string roleName)
    {
        var role = new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = roleName };
        await _roleManager.CreateAsync(role);
    }

    public async Task SeedSkinComplexionsAsync()
    {
        if (!_context.SkinComplexions.Any())
        {
            await _context.SkinComplexions.AddRangeAsync([
                new SkinComplexion { ScalePosition = "Monk 01", HexCode = "#F6EDE4" },
                new SkinComplexion { ScalePosition = "Monk 02", HexCode = "#F3E7DB" },
                new SkinComplexion { ScalePosition = "Monk 03", HexCode = "#F7EAD0" },
                new SkinComplexion { ScalePosition = "Monk 04", HexCode = "#EADABA" },
                new SkinComplexion { ScalePosition = "Monk 05", HexCode = "#D7BD96" },
                new SkinComplexion { ScalePosition = "Monk 06", HexCode = "#A07E56" },
                new SkinComplexion { ScalePosition = "Monk 07", HexCode = "#825C43" },
                new SkinComplexion { ScalePosition = "Monk 08", HexCode = "#604134" },
                new SkinComplexion { ScalePosition = "Monk 09", HexCode = "#3A312A" },
                new SkinComplexion { ScalePosition = "Monk 10", HexCode = "#292420" },
            ]);
        }
    }

    public async Task RunAllAsync() 
    {
        await SeedUsersAsync();
        await SeedAttractionsAsync();
        await SeedCausesAsync();
        await SeedSkinComplexionsAsync();
        await _context.SaveChangesAsync();
    }
}