using MySqlConnector;
using System.Text;

namespace FinderBE.Domain;

public class DatabaseConnectionFactory<T> : IDatabaseConnectionFactory<T>
{
    private string _connectionString;
    private IConfiguration _configuration;
    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        var sb = new StringBuilder();
        sb.Append(_configuration.GetConnectionString("DefaultConnection"));
        var modelName = typeof(T).Name;
        sb.Append($"Database={modelName.ToLower()}s");
        _connectionString = sb.ToString();
    }

    public MySqlConnection OpenConnection()
    {
        try
        {
            var connection = new MySqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
        catch (MySqlException ex)
        {
            var message = ex.Number switch
            {
                1045 => "Access denied for this user",
                1040 => "Too many connections, please shut some down",
                2003 => "Server shut down"
            };
            throw new Exception($"Error establishing database connection {message}");

        }
    }
}
