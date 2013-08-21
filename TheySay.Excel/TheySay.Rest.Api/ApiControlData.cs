using System;

namespace TheySay.Rest.Api
{
    public class ApiControlData
    {
        public long WindowRemaining { get; set; }
        public long WindowLimit { get; set; }
        public DateTime NextWindowTime { get; set; }

        public long IntervalInSecs { get; set; }

        public long DayRemaining { get; set; }
        public long DayLimit { get; set; }
        public DateTime DayWindowResetTime { get; set; }
    }
}