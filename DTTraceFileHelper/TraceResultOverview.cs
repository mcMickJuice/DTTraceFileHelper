using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTraceFileHelper
{
    public class TraceResultOverview
    {
        public string TracedAddress { get; set; }
        public int NumberOfFoundAddresses { get; set; }
        public string DahlId { get; private set; }
        public string AddressInfoId { get; private set; }

        public TraceResultOverview(string dahlId, string addressInfoId)
        {
            DahlId = dahlId;
            AddressInfoId = addressInfoId;
        }
    }
}
