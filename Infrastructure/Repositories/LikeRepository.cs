using System.Data;
using Dapper;

namespace Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly IDbConnection _dbConnection;

    public LikeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> GetLikesCountAsync(int dogOwnerId)
    {
        var query = "SELECT COUNT(*) FROM Likes WHERE DogOwner = @DogOwnerId";
        return await _dbConnection.ExecuteScalarAsync<int>(query, new { DogOwnerId = dogOwnerId });
    }
}