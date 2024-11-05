using System.Data;
using Dapper;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var query = "SELECT * FROM Users WHERE Id = @UserId";
        return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
    }
}