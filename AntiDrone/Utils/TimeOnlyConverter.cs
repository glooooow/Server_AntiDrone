using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AntiDrone.Utils;

public class TimeOnlyConverter : ValueConverter<TimeOnly, DateTime>
{
    public TimeOnlyConverter() : base(
        timeOnly => new DateTime().AddTicks(timeOnly.Ticks),
        dateTime => TimeOnly.FromDateTime(dateTime))
    {
    }
}