using System.Collections.Generic;

namespace CalcTime.Entity
{
    public class Timeline
    {
        public class UTC
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public List<UTC> UTCs { get; set; }
    }
}
