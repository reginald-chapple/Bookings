using System.Security.Claims;
using Bookings.Web.Models;

namespace Bookings.Web.Data.Services;

public interface IUserService
{
    UserModel GetUserModel(string userId);
    Task<CreatorModel> GetCreatorAsync(string createdBy);
    Task CreateSuperUserAsync();
    string GetCurrentUserId(ClaimsPrincipal User);
}
