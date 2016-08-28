using System;

namespace GuildWars2Orchestra.Extensions
{
    public static class TimeSpanExtension
    {
        public static TimeSpan Multiply(this TimeSpan timeSpan, int multiplier)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks*multiplier);
        }

        public static TimeSpan Divide(this TimeSpan timeSpan, int dividor)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks/dividor);
        }
    }
}