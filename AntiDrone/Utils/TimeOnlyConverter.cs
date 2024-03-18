using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AntiDrone.Utils;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter() : base(
        timeOnly => new TimeSpan(timeOnly.Ticks),
        timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        //dateTime => TimeOnly.FromDateTime(dateTime))
    {
    }
}