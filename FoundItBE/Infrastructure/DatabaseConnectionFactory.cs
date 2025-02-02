using MySqlConnector;
using System.Text;

namespace FoundItBE.Infrastructure;

public class DatabaseConnectionFactory<T> : IDatabaseConnectionFactory<T>
{
    private string _connectionString;
    private IConfiguration _configuration;
    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        //var sb = new StringBuilder();
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
        //_connectionString = sb.ToString();
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
