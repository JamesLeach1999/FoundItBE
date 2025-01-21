using MySqlConnector;
using System.Data.Common;

namespace FinderBE.Helpers;

public class CustomOrm<T> : ICustomOrm<T>
{
    public async Task<List<T>> MapSqlValues(DbDataReader reader, Func<DbDataReader, T> mapper)
    {
        try
        {
            var results = new List<T>();

            while (await reader.ReadAsync())
            {
                results.Add(mapper(reader));
            }

            return results;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error mapping returned value {typeof(T).Name}");
        }

    }
}
