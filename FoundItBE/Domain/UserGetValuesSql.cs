using FoundItBE.Helpers;
using FoundItBE.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
namespace FoundItBE.Domain;

public class UserGetValuesSql(IDatabaseConnectionFactory<User> _sqlDbConnection) : IGetValues<User>
{
    public async Task<List<User>> GetValues()
    {
        try
        {
            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var users = await sqlConnection.QueryAsync<User>("SELECT * FROM users;");

            return users.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Error executing query: ", ex);
        }

    }

    public async Task<User> GetValue(Guid userId)
    {
        try
        {
            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var user = await sqlConnection.QueryAsync<User>("SELECT * FROM users WHERE UserId = @userId", new {userId = userId});

            return user.Count() == 0 ? null : user.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting single user", ex);
        }
    }
}
