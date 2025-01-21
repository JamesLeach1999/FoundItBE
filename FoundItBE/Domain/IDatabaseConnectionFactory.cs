using MySqlConnector;

namespace FoundItBE.Domain;

public interface IDatabaseConnectionFactory<ModelType>
{
    public MySqlConnection OpenConnection();
}
