using FinderBE.Helpers;
using FinderBE.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
namespace FinderBE.Domain;

public class UserGetValuesSql(IDatabaseConnectionFactory<User> _sqlDbConnection, ICustomOrm<User> _customOrm) : IGetValues<User>
{
    public async Task<List<User>> GetValues()
    {
        try
        {
            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var users = await sqlConnection.QueryAsync<User>("SELECT * FROM users");

            return users.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("error", ex);
        }

    }

    public async Task<User> GetValue(Guid userId)
    {
        try
        {
            var query = "SELECT * FROM users.users WHERE userId = @userId";

            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var user = await sqlConnection.QueryAsync<User>("SELECT * FROM users.users WHERE userId = @userId", new {userId});

            return user == null ? throw new Exception("No user found") : user.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting single user", ex);
        }
    }
}
