using MySqlConnector;
using System.Text;

namespace FoundItBE.Infrastructure;

public class MySqlConnection<T> : IDatabaseConnectionFactory<T>
{
    private string _connectionString;
    private IConfiguration _configuration;
    public MySqlConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
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
                1049 => "Database not found",
                2003 => "Server shut down",
                _ => ex.Message
            };
            throw new Exception($"Error establishing mysql connection: {message}");

        }
    }
}
