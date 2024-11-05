using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IUserService
{
    Task<User> GetUserProfileAsync(int userId);
}