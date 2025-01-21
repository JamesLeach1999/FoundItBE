using Dapper;
using FinderBE.Models;
using MySqlConnector;

namespace FinderBE.Domain;

public class UserPostValuesSql(IDatabaseConnectionFactory<User> _sqlDbConnection) : ICreateValues<User, object>
{
    public object PostUser(User user)
    {
        try
        {
            using var sqlConnection = _sqlDbConnection.OpenConnection();

            var sqlQuery = "INSERT INTO users (UserId, AccountCreatedDate, Email, Password, Username) VALUES (@UserId, @AccountCreatedDate, @Email, @Password, @Username)";

            var id = Guid.NewGuid();
            Console.Write(id);
            var newUser = new User() { UserId = id, AccountCreatedDate = DateTime.Now, Email = user.Email, Password = user.Password, Username = user.Username };

            var rowsAffected = sqlConnection.Execute(sqlQuery, new { UserId = id, AccountCreatedDate = DateTime.Now, user.Email, user.Password, user.Username });

            return rowsAffected;
        }
        catch (MySqlException ex) {
            var message = ex.Number switch
            {
                1114 => "Table is full please delete some records",
                1064 => "Syntax error",
                1062 => "Duplicate email address"
            };

            throw new Exception($"Error creating user {message}");
        }
    }
}
