using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Converter
{
    public static class DateTimeConverter
    {
        /// <summary>
        /// Convert UtcTime To LocalTime
        /// </summary>
        /// <param name="utcTime">Utc Time <see cref=""></see></param>
        /// <returns></returns>
        public static DateTime ToLocalTime(this DateTime utcTime)
        {
            return utcTime;
        }
    }
}
