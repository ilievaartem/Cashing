namespace Infrastructure.Repositories;

public interface ILikeRepository
{
    Task<int> GetLikesCountAsync(int dogOwnerId);
}