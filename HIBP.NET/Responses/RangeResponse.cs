using System;
using System.Collections.Generic;
using System.Text;

namespace HIBP.Responses
{
    public class RangeResponse
    {
        public RangeResponse(string sHA1, int timesSeen)
        {
            SHA1 = sHA1;
            TimesSeen = timesSeen;
        }

        public string SHA1 { get; private set; }
        public int TimesSeen { get; private set; }
    }
}
