using MySqlConnector;

namespace FinderBE.Domain;

public interface IDatabaseConnectionFactory<ModelType>
{
    public MySqlConnection OpenConnection();
}
