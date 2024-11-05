namespace Infrastructure.Services;

public interface ILikeService
{
    Task<int> GetLikesCountAsync(int dogOwnerId);
}