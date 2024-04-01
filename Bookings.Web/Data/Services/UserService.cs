using System.Security.Claims;
using Bookings.Web.Domain;
using Bookings.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public UserModel GetUserModel(string userId)
        {
            var userModel =  _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserModel
            {
                Name = u.FullName,
                UserName = u.UserName!,
                Photo = u.Image
            }).FirstOrDefault();
            
            return userModel!;
        }

        public async Task CreateSuperUserAsync()
        {
            if (!_context.Users.Any() && !_context.Roles.Any())
            {
                var role = new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator"
                };

                await _roleManager.CreateAsync(role);

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = "Super User",
                    UserName = "sudo@local.com",
                    Email = "sudo@local.com"
                };

                await _userManager.CreateAsync(user, "P@ss1234");
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        public string GetCurrentUserId(ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<CreatorModel> GetCreatorAsync(string createdBy)
        {
            var user = await _context.Users
                .Where(u => u.Id == createdBy)
                .Select(u => new CreatorModel { Id = u.Id, Email = u.Email!, FullName = u.FullName, Image = u.Image })
                .FirstOrDefaultAsync();
            return user!;
        }
    }
}