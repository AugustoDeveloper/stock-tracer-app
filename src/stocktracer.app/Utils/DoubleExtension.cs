using System;

namespace stocktracer.app.Utils
{
    static public class DoubleExtension
    {
        static public DateTime ToDatetime(this double epoch)
        {
            var minValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var currentValue = minValue.AddSeconds( epoch ).ToLocalTime();
            return currentValue;
        }
    }
}