using MySqlConnector;
using System.Data.Common;
namespace FoundItBE.Helpers;

public interface ICustomOrm<T>
{
    Task<List<T>> MapSqlValues(DbDataReader reader, Func<DbDataReader, T> mapper);
}
