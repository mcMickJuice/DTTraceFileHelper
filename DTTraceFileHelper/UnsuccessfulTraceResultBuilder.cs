using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTTraceFileHelper;
using DTTraceFileHelper.TraceFileSpecs;

namespace DTTraceFileHelper
{
    public class UnsuccessfulTraceResultBuilder : ITraceResultBuilder
    {
        private TraceAddress _traceAddress;
        private TraceResultOverview _traceResultOverview;

        public UnsuccessfulTraceResultBuilder(TraceAddress traceAddress)
        {
            _traceAddress = traceAddress;
            _traceResultOverview = new TraceResultOverview(_traceAddress.DahlId, _traceAddress.AddressInfoId);
        }

        public string GetTraceResultBlock(IAddressProvider addressProvider)
        {
            var traceRecordHelper = new IncomingTraceRecordCreator(0, _traceAddress, addressProvider);
            return traceRecordHelper.GetTraceHeader();
        }

        public TraceResultOverview GetTraceResultOverview()
        {
            return _traceResultOverview;
        }
    }
}
