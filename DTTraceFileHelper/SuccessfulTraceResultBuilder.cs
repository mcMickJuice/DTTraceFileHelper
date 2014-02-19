using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTTraceFileHelper;
using DTTraceFileHelper.TraceFileSpecs;

namespace DTTraceFileHelper
{
    public class SuccessfulTraceResultBuilder : ITraceResultBuilder
    {
        private readonly TraceAddress _traceAddress;
        private readonly TraceResultOverview _traceResultOverview;
        private string _traceResultBlock;

        public SuccessfulTraceResultBuilder(TraceAddress traceAddress)
        {
            _traceAddress = traceAddress;
            _traceResultOverview = new TraceResultOverview(traceAddress.DahlId, traceAddress.AddressInfoId);
        }
        
        public string GetTraceResultBlock(IAddressProvider addressProvider)
        {
            var numberOfAddresses = GetNumberOfAddresses();
            var traceRecordHelper = new IncomingTraceRecordCreator(numberOfAddresses, _traceAddress, addressProvider);
            _traceResultOverview.NumberOfFoundAddresses = numberOfAddresses;
            _traceResultBlock += traceRecordHelper.GetTraceHeader() + "\r\n";

            for (var i = 0; i < numberOfAddresses; i++)
            {

                _traceResultBlock += traceRecordHelper.GetAddressRecord(i);

                if ((i + 1) != numberOfAddresses)
                {
                    _traceResultBlock += "\r\n";
                }
            }

            return _traceResultBlock;
        }

        public TraceResultOverview GetTraceResultOverview()
        {
            return _traceResultOverview;
        }

        private static int GetNumberOfAddresses()
        {
            var randomValue = RandomGenerator.GetRandomNumber(1, 6);
            return randomValue;
        }
    }
}
