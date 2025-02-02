using Dapper;
using System.Data;
namespace FoundItBE.Helpers;
public class MySqlGuidTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.Value = value?.ToString();
    }

    public object Parse(Type destinationType, object value)
    {
        Console.WriteLine(value.GetType().Name);
        if (value is string stringValue)
        {
            stringValue = stringValue.Trim(); // Clean the value
            if (Guid.TryParse(stringValue, out var guid))
            {
                return guid;
            }
            throw new FormatException($"Cannot parse '{stringValue}' as GUID.");
        }

        return value;
    }
}