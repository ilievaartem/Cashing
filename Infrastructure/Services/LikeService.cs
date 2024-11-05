using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IDistributedCache _distributedCache;
    private const string LikesCacheKey = "LikesCount_";

    public LikeService(ILikeRepository likeRepository, IDistributedCache distributedCache)
    {
        _likeRepository = likeRepository;
        _distributedCache = distributedCache;
    }

    public async Task<int> GetLikesCountAsync(int dogOwnerId)
    {
        var cacheKey = $"{LikesCacheKey}{dogOwnerId}";
        var likesCountString = await _distributedCache.GetStringAsync(cacheKey);

        if (likesCountString == null)
        {
            Console.WriteLine("Cache miss: fetching likes count from DB");
            int likesCount = await _likeRepository.GetLikesCountAsync(dogOwnerId);
            await _distributedCache.SetStringAsync(cacheKey, likesCount.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
            return likesCount;
        }

        Console.WriteLine("Cache hit: fetching likes count from Redis");
        return int.Parse(likesCountString);
    }

}