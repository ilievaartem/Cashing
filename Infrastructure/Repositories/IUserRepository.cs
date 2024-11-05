using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int userId);
}