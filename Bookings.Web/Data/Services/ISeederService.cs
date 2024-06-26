namespace Bookings.Web.Data.Services;

public interface ISeederService
{
    Task SeedCausesAsync();
    Task SeedValuesAsync();
    Task SeedUsersAsync();
    Task CreateUserAsync(string fullName, string userName, string email, string password, string imageName, string role);
    Task CreateRoleAsync(string roleName);
    Task RunAllAsync();
}