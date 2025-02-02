using Dapper;
using FoundItBE.Infrastructure;
using FoundItBE.Models;
using MySqlConnector;

namespace FoundItBE.Domain;

public class UserPostValuesSql(IDatabaseConnectionFactory<User> _sqlDbConnection) : ICreateValues<UserRequest, object>
{
    public object PostUser(UserRequest user)
    {
        try
        {
            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var sqlQuery = "INSERT INTO users (UserId, AccountCreatedDate, Email, Password, Username) VALUES (@UserId, @AccountCreatedDate, @Email, @Password, @Username)";

            var newUser = new User() { UserId = Guid.NewGuid(), AccountCreatedDate = DateTime.Now, Email = user.Email, Password = user.Password, Username = user.Username };

            sqlConnection.Execute(sqlQuery, newUser);

            return user;
        }
        catch (MySqlException ex) {
            var message = ex.Number switch
            {
                1114 => "Table is full please delete some records",
                1064 => "Syntax error",
                1062 => "Duplicate email address",
                _ => "Thats numberwang"
            };

            throw new Exception($"Error creating user: {message}");
        }
    }
}
