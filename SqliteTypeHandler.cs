using Dapper;
using System.Data;

namespace CodingTracker;

abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public override void SetValue(IDbDataParameter parameter, T? value)
    => parameter.Value = value;
}

class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
        => TimeSpan.Parse((string)value);
}
