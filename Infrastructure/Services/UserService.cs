using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private const string UserCacheKey = "UserProfile_";

    public UserService(IUserRepository userRepository, IMemoryCache memoryCache)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
    }

    public async Task<User> GetUserProfileAsync(int userId)
    {
        var cacheKey = $"{UserCacheKey}{userId}";
        
        if (!_memoryCache.TryGetValue(cacheKey, out User user))
        {
            Console.WriteLine("Cache miss: fetching user profile from DB");
            user = await _userRepository.GetUserByIdAsync(userId);

            if (user != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                    
                _memoryCache.Set(cacheKey, user, cacheOptions);
            }
        }
        else
        {
            Console.WriteLine("Cache hit: fetching user profile from in-memory cache");
        }
        return user;
    }
    
}