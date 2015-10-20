using System;

namespace CalcTime.Entity
{
    public class TimeValue
    {
        public DateTime ReturnTime { get; set; }
        public string TimelineValue { get; set; }
        public string TimeFormat { get; set; }
        public string ConvertingTime { get; set; }
        public bool IsInvalid { get; set; }
    }
}
