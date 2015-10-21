using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CalcTime.Entity
{
    public class Format
    {
        public class JsonData
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Example { get; set; }
        }

        public List<JsonData> FormatList { get; set; }
    }
}
