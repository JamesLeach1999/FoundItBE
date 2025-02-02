using MySqlConnector;

namespace FoundItBE.Infrastructure;

public interface IDatabaseConnectionFactory<ModelType>
{
    public MySqlConnection OpenConnection();
}
