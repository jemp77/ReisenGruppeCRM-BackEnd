using System;

namespace SmartCardCRM.Util
{
    public static class CustomDateTime
    {
        public static DateTime Now
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "SA Pacific Standard Time");
            }
        }
    }
}
