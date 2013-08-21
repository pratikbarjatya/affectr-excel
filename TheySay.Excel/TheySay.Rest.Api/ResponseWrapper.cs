using System;

namespace TheySay.Rest.Api
{
    public class ResponseWrapper<T> where T : class
    {
        private readonly long[] _data = new long[Enum.GetNames(typeof(Limit.ELimit)).Length];

        public ResponseWrapper(T resp)
        {
            Response = resp;
        }
        public ResponseWrapper(T resp, AnalysisCell input)
        {
            Response = resp;
            Input = input;
        }

        public AnalysisCell Input { get; set; }
        public long Limit { get { return this[Api.Limit.ELimit.DayLimit]; } }
        public long Remaining { get { return this[Api.Limit.ELimit.DayRemaining]; } }
        public DateTime ResetTime { get { return FromUnixTime(this[Api.Limit.ELimit.DayLimitResetTime]); } }
        public long IntervalLengthSecs { get { return this[Api.Limit.ELimit.RateLimitInterval]; } }
        public long WindowLimit { get { return this[Api.Limit.ELimit.WindowRateLimit]; } }
        public long WindowRemaining { get { return this[Api.Limit.ELimit.WindowLimitRemaining]; } }
        public DateTime WindowResetTime { get { return FromUnixTime(this[Api.Limit.ELimit.WindowLimitResetTime]); } }

        public bool Success { get { return Response != null; } }
        public string ErrorMessage { get; set; }

        public T Response { get; private set; }

        public long this[Limit.ELimit val]
        {
            get
            {
                return _data[(int) val - 1];
            }
            set
            {
                _data[(int) val - 1] = value;
            }
        }

        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

    }
}